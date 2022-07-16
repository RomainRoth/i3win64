using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace i3win64
{
    /// <summary>
    /// So far this piece is used to identify new windows and callback the code to steal it
    /// It also looks for closed windows to remove them from the tiles
    /// For debug purposes it's only checking some parts
    /// </summary>
    internal class HWindowRouter
    {
        #region PInvokeImports
        // Imports
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        #endregion

        #region LocalVariables
        // Windows Database
        private List<IntPtr> windowDb = new List<IntPtr>();
        #endregion
        
        #region Attributes
        // Thread abortion
        private bool keepAlive = true;
        /// <summary>
        /// Set to false to exit the background thread
        /// </summary>
        public bool KeepAlive { get => keepAlive; set => keepAlive = value; }
        #endregion

        #region Events
        public EventHandler<IntPtr>? CreateWindow;
        public virtual void OnCreateWindow(Object sender, IntPtr e) => CreateWindow?.Invoke(sender, e);
        public EventHandler<IntPtr>? CloseWindow;
        public virtual void OnCloseWindow(Object sender, IntPtr e) => CloseWindow?.Invoke(sender, e);
        #endregion

        #region SingletonStructure
        // Singleton Structure
        private static readonly Lazy<HWindowRouter> instance = new Lazy<HWindowRouter>(() => new HWindowRouter());
        public static HWindowRouter Instance => instance.Value;
        private HWindowRouter() { }
        #endregion

        #region Methods
        public void Run()
        {
            // While thread is kept alive
            while(keepAlive)
            {
                // Drop dead windows from db
                for (int i = windowDb.Count - 1; i >= 0; i--)
                {
                    if (!windowDb[i].IsVisible())
                    {
                        OnCloseWindow(this, windowDb[i]);
                        windowDb.RemoveAt(i);
                    }
                        
                }
                
                // List all open windows
                EnumWindows((hWnd, lParam) =>
                {
                    // If the window is visible
                    if (hWnd.IsVisible())
                    {
                        // If the window title contains Notepad
                        if (hWnd.GetWindowTitle().Contains("Notepad"))
                        {
                            // Store in the the db
                            if (!windowDb.Contains(hWnd))
                            {
                                windowDb.Add(hWnd);
                                OnCreateWindow(this, hWnd);
                            }
                        }
                    }
                    return true;
                }, IntPtr.Zero);

                // Give the CPU some rest so it doesn't burn
                Thread.Sleep(1);
            }

        }
        #endregion
    }
}
