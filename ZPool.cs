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
                PInvokeDb.KeyModifier modifier = (PInvokeDb.KeyModifier)((int)m.LParam & 0xFFFF);
                int id = m.WParam.ToInt32();
                
                if(key==Keys.F && modifier==PInvokeDb.KeyModifier.None)
                    MessageBox.Show("Global shortcut test");
            }
        }

        private void ZPool_Load(object sender, EventArgs e)
        {
            // ToDo : Check for registry key WriteRegStr HKCU "Software\Microsoft\Windows NT\CurrentVersion\Winlogon" "Shell"
            // If key <> Current path, ask permission from user to update the key
            // .NET 6 wraps software in a dll, be sure to bind .exe to registry, not .dll
            // MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);

            /*PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)(PInvokeDb.KeyModifier.WinKey | PInvokeDb.KeyModifier.Alt),
                (int)Keys.F);*/

            /*PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)(PInvokeDb.KeyModifier.None),
                (int)Keys.F);*/

            PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)(PInvokeDb.KeyModifier.Alt),
                (int)Keys.Tab);

            zScreen.Show();

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
                        for(int i=0; i<=zScreen.Controls.Count; i++)
                        {
                            ZTile zTile = (ZTile)zScreen.Controls[i];
                            if(zTile.HWdn == e)
                            {
                                zScreen.Controls.Remove(zTile);
                            }
                        }
                        //zScreen.Controls.Remove(zScreen.panel1); 
                    };
                    zScreen.Invoke(safeBind);
                }
                else
                {
                    for (int i = 0; i <= zScreen.Controls.Count; i++)
                    {
                        ZTile zTile = (ZTile)zScreen.Controls[i];
                        if (zTile.HWdn == e)
                        {
                            zScreen.Controls.Remove(zTile);
                        }
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

        private void ZPool_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ZPool_FormClosing(object sender, FormClosingEventArgs e)
        {
            HWindowRouter.Instance.KeepAlive = false;
            PInvokeDb.UnregisterHotKey(this.Handle, 1);
        }
    }
}
