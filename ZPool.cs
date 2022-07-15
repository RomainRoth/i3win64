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

        private void ZPool_Load(object sender, EventArgs e)
        {
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
    }
}
