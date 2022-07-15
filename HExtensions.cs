using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace i3win64
{
    public static class HExtensions
    {
        /*   ________              __                                    __                               
            /        |            /  |                                  /  |                              
            $$$$$$$$/  __    __  _$$ |_     ______   _______    _______ $$/   ______   _______    _______ 
            $$ |__    /  \  /  |/ $$   |   /      \ /       \  /       |/  | /      \ /       \  /       |
            $$    |   $$  \/$$/ $$$$$$/   /$$$$$$  |$$$$$$$  |/$$$$$$$/ $$ |/$$$$$$  |$$$$$$$  |/$$$$$$$/ 
            $$$$$/     $$  $$<    $$ | __ $$    $$ |$$ |  $$ |$$      \ $$ |$$ |  $$ |$$ |  $$ |$$      \ 
            $$ |_____  /$$$$  \   $$ |/  |$$$$$$$$/ $$ |  $$ | $$$$$$  |$$ |$$ \__$$ |$$ |  $$ | $$$$$$  |
            $$       |/$$/ $$  |  $$  $$/ $$       |$$ |  $$ |/     $$/ $$ |$$    $$/ $$ |  $$ |/     $$/ 
            $$$$$$$$/ $$/   $$/    $$$$/   $$$$$$$/ $$/   $$/ $$$$$$$/  $$/  $$$$$$/  $$/   $$/ $$$$$$$/    */
        #region Extensions
        
        /// <summary>
        /// Returns true if the window has the WS_VISIBLE flag
        /// </summary>
        /// <param name="hWnd">Window Handle</param>
        /// <returns>Is WS_VISIBLE set ?</returns>
        public static bool IsVisible(this IntPtr hWnd)
        {
            return (GetWindowLongPtr(hWnd, (int)GWL.GWL_STYLE) & (uint)WS.WS_VISIBLE) != 0;
        }

        public static uint GetWindowStyles(this IntPtr hWnd)
        {
            return GetWindowLongPtr(hWnd, (int)GWL.GWL_STYLE);
        }
        
        public static uint GetWindowStylesEx(this IntPtr hWnd)
        {
            return GetWindowLongPtr(hWnd, (int)GWL.GWL_EXSTYLE);
        }

        public static string GetWindowTitle(this IntPtr hWnd)
        {
            int textLength = GetWindowTextLength(hWnd);
            StringBuilder text = new StringBuilder(textLength);
            GetWindowText(hWnd, text, textLength + 1);
            return text.ToString();
        }

        public static string GetWindowClass(this IntPtr hWnd)
        {
            StringBuilder classn = new StringBuilder(256);
            GetClassName(hWnd, classn, 256 + 1);
            return classn.ToString();
        }

        public static IntPtr AttachTo(this IntPtr hWnd, IntPtr hWndParent)
        {
            SetParent(hWnd, hWndParent);
            return hWnd;
        }
        public static IntPtr MoveTo(this IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint)
        {
            MoveWindow(hWnd, X, Y, nWidth, nHeight, bRepaint);
            return hWnd;
        }

        public static IntPtr StripTitle(this IntPtr hWdn)
        {
            // Perhaps a simpler way exists but I don't know it yet.
            // I'm not too familiar with bitwise operations so I'm not sure how to do this.

            // WS Cleanup
            uint windowStyle = hWdn.GetWindowStyles();
            uint newWindowStyle = 0;
            foreach (WS ws in (WS[])Enum.GetValues(typeof(WS)))
            {
                switch (ws)
                {
                    case WS.WS_MAXIMIZEBOX:
                    case WS.WS_MINIMIZEBOX:
                    case WS.WS_CAPTION:
                    case WS.WS_SYSMENU:
                    case WS.WS_SIZEFRAME:
                    case WS.WS_DLGFRAME:
                    case WS.WS_BORDER:
                    case WS.WS_OVERLAPPEDWINDOW:
                    case WS.WS_POPUPWINDOW:
                        break;
                    default:
                        if ((windowStyle & ((uint)ws)) != 0u) newWindowStyle = newWindowStyle | (uint)ws;
                        break;
                }
            }
            SetWindowLongPtr(hWdn,(int)GWL.GWL_STYLE,(IntPtr)newWindowStyle);
            // WSE Cleanup
            uint windowStyleEx = hWdn.GetWindowStylesEx();
            uint newWindowStyleEx = 0;
            foreach (WSE wse in (WSE[])Enum.GetValues(typeof(WSE)))
            {
                switch (wse)
                {
                    case WSE.WS_EX_APPWINDOW:
                    case WSE.WS_EX_WINDOWEDGE:
                    case WSE.WS_EX_CLIENTEDGE:
                    case WSE.WS_EX_OVERLAPPEDWINDOW:
                        break;
                    default:
                        if ((windowStyleEx & ((uint)wse)) != 0u) newWindowStyleEx = newWindowStyleEx | (uint)wse;
                        break;
                }
            }
            SetWindowLongPtr(hWdn,(int)GWL.GWL_EXSTYLE,(IntPtr)newWindowStyleEx);
            return hWdn;
        }

        #endregion

        /*   _______   ______                                __                 
            /       \ /      |                              /  |                
            $$$$$$$  |$$$$$$/  _______   __     __  ______  $$ |   __   ______  
            $$ |__$$ |  $$ |  /       \ /  \   /  |/      \ $$ |  /  | /      \ 
            $$    $$/   $$ |  $$$$$$$  |$$  \ /$$//$$$$$$  |$$ |_/$$/ /$$$$$$  |
            $$$$$$$/    $$ |  $$ |  $$ | $$  /$$/ $$ |  $$ |$$   $$<  $$    $$ |
            $$ |       _$$ |_ $$ |  $$ |  $$ $$/  $$ \__$$ |$$$$$$  \ $$$$$$$$/ 
            $$ |      / $$   |$$ |  $$ |   $$$/   $$    $$/ $$ | $$  |$$       |
            $$/       $$$$$$/ $$/   $$/     $/     $$$$$$/  $$/   $$/  $$$$$$$/     */
        #region PInvokeImports

        /// <summary>
        /// Retrieves information about the specified window. 
        /// The function also retrieves the value at a specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR.</param>
        /// <returns>If the function succeeds, the return value is the requested value. 
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
        private static uint GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLong32(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern uint GetWindowLongPtr64(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);


        /// <summary>
        /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs. 
        /// The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. 
        /// Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified offset.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
        private static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        /// <summary>
        /// Attach a window to the current process.
        /// </summary>
        /// <param name="hWndChild">Window Handle</param>
        /// <param name="hWndNewParent">Parent Component Handle</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        /// <summary>
        /// Moves window to desired position TODO: call on parent resize
        /// </summary>
        /// <param name="hWnd">Window Handle</param>
        /// <param name="X">Abs</param>
        /// <param name="Y">Ord</param>
        /// <param name="nWidth">Width</param>
        /// <param name="nHeight">Height</param>
        /// <param name="bRepaint">Redraw</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        /// <summary>
        /// Needs more studying, might be what defines what is a window or not.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Get Window title string size
        /// </summary>
        /// <param name="hWnd">Window Handle</param>
        /// <returns>Window title string size for StringBuilder</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Get Window title
        /// </summary>
        /// <param name="hWnd">Window Handle</param>
        /// <param name="lpString">StringBuilder</param>
        /// <param name="nMaxCount">SizeOf -> GetWindowTextLength</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        #endregion
    }
}
