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
        ProcessRouter processRouter = new ProcessRouter();
        ZScreen zScreen = new ZScreen();

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
            /*PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)(PInvokeDb.KeyModifier.WinKey | PInvokeDb.KeyModifier.Alt),
                (int)Keys.F);*/

            PInvokeDb.RegisterHotKey(this.Handle, 1,
                (int)(PInvokeDb.KeyModifier.None),
                (int)Keys.F);

            zScreen.Show();

            processRouter.WindowAttached += (s, e) =>
            {
                if(zScreen.panel1.InvokeRequired)
                {
                    Action safeBind = delegate { PInvokeDb.SetParent(e.WindowHandle, zScreen.panel1.Handle); };
                    zScreen.panel1.Invoke(safeBind);
                }
                else
                {
                    PInvokeDb.SetParent(e.WindowHandle, zScreen.panel1.Handle);
                }

            };
            
            Thread thread = new Thread(() =>
            {
                while(true)
                {
                    processRouter.Run();
                    Thread.Sleep(1);
                };
            });
            //thread.Start();
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            PInvokeDb.SetParent((IntPtr)listBoxWindows.SelectedItem, zScreen.panel1.Handle);
            // WindowStyleEx redefining
            /*uint windowStyleEx = PInvokeDb.GetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_EXSTYLE);
            windowStyleEx = windowStyleEx | (uint)PInvokeDb.WindowStylesEx.WS_EX_TOOLWINDOW;
            if(windowStyleEx & (uint)PInvokeDb.WindowStylesEx.WS_EX_STATICEDGE)*/
            /*PInvokeDb.SetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_EXSTYLE,
                (IntPtr)PInvokeDb.WindowStylesEx.WS_EX_CLIENTEDGE
                );*/
            // WindowStyle redefining
            uint windowStyle = PInvokeDb.GetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_STYLE);
            uint newWindowStyle = 0;
            foreach(PInvokeDb.WindowStyles ws in (PInvokeDb.WindowStyles[]) Enum.GetValues(typeof(PInvokeDb.WindowStyles)))
            {
                switch(ws)
                {
                    case PInvokeDb.WindowStyles.WS_MAXIMIZEBOX:
                    case PInvokeDb.WindowStyles.WS_MINIMIZEBOX:
                    case PInvokeDb.WindowStyles.WS_CAPTION:
                    case PInvokeDb.WindowStyles.WS_SYSMENU:
                    case PInvokeDb.WindowStyles.WS_SIZEFRAME:
                        break;
                    default:
                        if ((windowStyle & ((uint)ws)) != 0u) newWindowStyle = newWindowStyle | (uint)ws;
                        break;
                }
            }
            newWindowStyle = newWindowStyle | (uint)PInvokeDb.WindowStyles.WS_POPUP;
            PInvokeDb.SetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_STYLE,
                (IntPtr)newWindowStyle
                );
            // WindowStyleEx redefining
            uint windowStyleEx = PInvokeDb.GetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_EXSTYLE);
            uint newWindowStyleEx = 0;
            foreach (PInvokeDb.WindowStylesEx wse in (PInvokeDb.WindowStylesEx[])Enum.GetValues(typeof(PInvokeDb.WindowStylesEx)))
            {
                switch (wse)
                {
                    case PInvokeDb.WindowStylesEx.WS_EX_APPWINDOW:
                        break;
                    default:
                        if ((windowStyleEx & ((uint)wse)) != 0u) newWindowStyleEx = newWindowStyleEx | (uint)wse;
                        break;
                }
            }
            newWindowStyleEx = newWindowStyleEx | (uint)PInvokeDb.WindowStylesEx.WS_EX_TOOLWINDOW;
            PInvokeDb.SetWindowLongPtr((IntPtr)listBoxWindows.SelectedItem,
                (int)PInvokeDb.WindowLongFlags.GWL_EXSTYLE,
                (IntPtr)newWindowStyleEx
                );
            // Resize & Redraw
            PInvokeDb.MoveWindow((IntPtr)listBoxWindows.SelectedItem, 0, 0, zScreen.panel1.Width, zScreen.panel1.Height, true);
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
                        if (PInvokeDb.IsWindow(hWnd))
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
            IntPtr wHdl = (IntPtr)listBoxWindows.SelectedItem;
            tbWindowID.Text = wHdl.ToString();

            int textLength = PInvokeDb.GetWindowTextLength(wHdl);
            StringBuilder text = new StringBuilder(textLength);
            PInvokeDb.GetWindowText(wHdl, text, textLength + 1);
            tbWindowName.Text = text.ToString();
            StringBuilder classn = new StringBuilder(256);
            PInvokeDb.GetClassName(wHdl, classn, 256 + 1);
            tbClassName.Text = classn.ToString();
        }

        private void ZPool_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ZPool_FormClosing(object sender, FormClosingEventArgs e)
        {
            PInvokeDb.UnregisterHotKey(this.Handle, 1);
        }
    }
}
