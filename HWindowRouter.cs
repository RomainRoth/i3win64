using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace i3win64
{
    internal class HWindowRouter
    {
        // Imports
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        
        // Windows Database
        private List<IntPtr> windowDb = new List<IntPtr>();

        // Thread abortion
        private bool keepAlive = true;
        /// <summary>
        /// Set to false to exit the background thread
        /// </summary>
        public bool KeepAlive { get => keepAlive; set => keepAlive = value; }

        // Singleton Structure
        private static readonly Lazy<HWindowRouter> instance = new Lazy<HWindowRouter>(() => new HWindowRouter());
        public static HWindowRouter Instance => instance.Value;

        

        private HWindowRouter()
        {
        }

        public void Run()
        {
            // While thread is kept alive
            while(keepAlive)
            {
                // Drop dead windows from db
                for (int i = windowDb.Count - 1; i >= 0; i--)
                {
                    if (!windowDb[i].IsVisible())
                        windowDb.RemoveAt(i);
                }
                
                // List all open windows
                EnumWindows((hWnd, lParam) =>
                {
                    // If the window is visible
                    if(hWnd.IsVisible())
                    {
                        // Store in the the db
                        if (!windowDb.Contains(hWnd))
                        {
                            windowDb.Add(hWnd);
                        }
                    }

                    return true;
                }, IntPtr.Zero);

                // Give the CPU some rest so it doesn't burn
                Thread.Sleep(1);
            }

        }
    }
}
