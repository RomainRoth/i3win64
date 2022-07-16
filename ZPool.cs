using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace i3win64
{
    public partial class ZPool : Form
    {
        ZScreen zScreen = new ZScreen();
        Thread bgRunner = new Thread(() =>
        {
            HWindowRouter.Instance.Run();
        });

        public ZPool()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            
            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                KM modifier = (KM)((int)m.LParam & 0xFFFF);
                int id = m.WParam.ToInt32();
                
                // ToDo : Update this to use settings instead of whatever
                switch(modifier)
                {
                    // Moves the control
                    case KM.Control:
                        switch (key)
                        {
                            case Keys.Up:
                                zScreen.MoveU(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Right:
                                zScreen.MoveR(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Down:
                                zScreen.MoveD(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Left:
                                zScreen.MoveL(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                        }
                        break;
                    // Resize the control
                    case KM.Shift:
                        switch(key)
                        {
                            case Keys.Up:
                                zScreen.ShrinkH(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Right:
                                zScreen.GrowW(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Down:
                                zScreen.GrowH(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                            case Keys.Left:
                                zScreen.ShrinkW(HWindowRouter.Instance.LastFocusedWindow);
                                break;
                        }
                        break;
                }
            }
        }

        private void ZPool_Load(object sender, EventArgs e)
        {
            // ToDo : Check for registry key WriteRegStr HKCU "Software\Microsoft\Windows NT\CurrentVersion\Winlogon" "Shell"
            // If key <> Current path, ask permission from user to update the key
            // .NET 6 wraps software in a dll, be sure to bind .exe to registry, not .dll
            // MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);


            // Ask system for global shortcuts
            PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)KM.Shift,
                (int)Keys.Up);
            PInvokeDb.RegisterHotKey(this.Handle, 2,
                (int)KM.Shift,
                (int)Keys.Right);
            PInvokeDb.RegisterHotKey(this.Handle, 3,
                (int)KM.Shift,
                (int)Keys.Down);
            PInvokeDb.RegisterHotKey(this.Handle, 4,
                (int)KM.Shift,
                (int)Keys.Left);
            PInvokeDb.RegisterHotKey(this.Handle, 5,
                (int)KM.Control,
                (int)Keys.Up);
            PInvokeDb.RegisterHotKey(this.Handle, 6,
                (int)KM.Control,
                (int)Keys.Right);
            PInvokeDb.RegisterHotKey(this.Handle, 7,
                (int)KM.Control,
                (int)Keys.Down);
            PInvokeDb.RegisterHotKey(this.Handle, 8,
                (int)KM.Control,
                (int)Keys.Left);
            zScreen.Show();

            HWindowRouter.Instance.FocusChange += (s, e) =>
            {

            };
            
            HWindowRouter.Instance.CreateWindow += (s, e) =>
            {
                if (zScreen.InvokeRequired)
                {
                    Action safeBind = delegate { zScreen.Controls.Add(new ZTile(e)); };
                    zScreen.Invoke(safeBind);
                }
                else
                {
                    zScreen.Controls.Add(new ZTile(e));
                }
            };

            HWindowRouter.Instance.CloseWindow += (s, e) =>
            {
                if (zScreen.InvokeRequired)
                {
                    Action safeBind = delegate 
                    {
                        ZTile? zTile = zScreen.FindZTile(e);
                        if(zTile != null)
                        {
                            zScreen.Controls.Remove(zTile);
                        }
                    };
                    zScreen.Invoke(safeBind);
                }
                else
                {
                    ZTile? zTile = zScreen.FindZTile(e);
                    if (zTile != null)
                    {
                        zScreen.Controls.Remove(zTile);
                    }
                }
            };
            
            bgRunner.Start();
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            /*IntPtr hWnd = (IntPtr)listBoxWindows.SelectedItem;

            hWnd.AttachTo(zScreen.panel1.Handle);
            hWnd.StripTitle();

            // Resize & Redraw
            zScreen.panel1.Resize += (s, e) =>
            {
                hWnd.MoveTo(0, 0, zScreen.panel1.Width, zScreen.panel1.Height, true);
            };
            hWnd.MoveTo(0, 0, zScreen.panel1.Width, zScreen.panel1.Height, true);*/
        }

        private void buttonRefreshProcesses_Click(object sender, EventArgs e)
        {
            listBoxProcesses.DataSource = Process.GetProcesses().ToList();
            listBoxProcesses.DisplayMember = "ProcessName";
        }

        private void listBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Process selectedProcess = (Process)listBoxProcesses.SelectedItem;

            List<IntPtr> windows = new List<IntPtr>();
            try
            {
                /*PInvokeDb.EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
                {
                    windows.Add(hWnd);
                    return true;
                }, IntPtr.Zero);*/

                foreach(ProcessThread pt in selectedProcess.Threads)
                {
                    PInvokeDb.EnumThreadWindows((uint) pt.Id, delegate (IntPtr hWnd, IntPtr lParam)
                    {
                        if (hWnd.IsVisible())
                        {
                            windows.Add(hWnd);
                        }
                        return true;
                    }, IntPtr.Zero);
                }
                //selectedProcess.Threads[0].Id
                
                /*PInvokeDb.EnumChildWindows(selectedProcess.MainWindowHandle, delegate (IntPtr hWnd, IntPtr lParam)
                {
                    windows.Add(hWnd);
                    return true;
                }, IntPtr.Zero);*/
            }
            catch(Exception)
            {
                
            }
            listBoxWindows.DataSource = windows;
        }

        private void listBoxWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntPtr hWnd = (IntPtr)listBoxWindows.SelectedItem;
            tbWindowID.Text = hWnd.ToString();

            tbWindowName.Text = hWnd.GetWindowTitle();
            tbClassName.Text = hWnd.GetWindowClass();
            tbMainWindow.Text = ((Process)listBoxProcesses.SelectedItem).MainWindowHandle.ToString();

            uint windowStyle = ((IntPtr)listBoxWindows.SelectedItem).GetWindowStyles();
            rtbWS.Text = "";
            foreach (WS ws in (WS[])Enum.GetValues(typeof(WS)))
            {
                if ((windowStyle & ((uint)ws)) != 0u)  rtbWS.AppendText(ws.ToString() + Environment.NewLine);
            }
            uint windowStyleEx = ((IntPtr)listBoxWindows.SelectedItem).GetWindowStylesEx();
            rtbWS_EX.Text = "";
            foreach (WSE wse in (WSE[])Enum.GetValues(typeof(WSE)))
            {
                if ((windowStyleEx & ((uint)wse)) != 0u) rtbWS_EX.AppendText(wse.ToString() + Environment.NewLine);
            }
        }

        private void ZPool_FormClosing(object sender, FormClosingEventArgs e)
        {
            HWindowRouter.Instance.KeepAlive = false;
            PInvokeDb.UnregisterHotKey(this.Handle, 1);
            PInvokeDb.UnregisterHotKey(this.Handle, 2);
            PInvokeDb.UnregisterHotKey(this.Handle, 3);
            PInvokeDb.UnregisterHotKey(this.Handle, 4);
            PInvokeDb.UnregisterHotKey(this.Handle, 5);
            PInvokeDb.UnregisterHotKey(this.Handle, 6);
            PInvokeDb.UnregisterHotKey(this.Handle, 7);
            PInvokeDb.UnregisterHotKey(this.Handle, 8);
        }
    }
}
