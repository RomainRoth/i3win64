using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace i3win64
{
    public class PInvokeDb
    {
        /// <summary>
        /// List all the windows of a thread
        /// </summary>
        /// <param name="dwThreadId">Thread identifier</param>
        /// <param name="lpfn">Callback</param>
        /// <param name="lParam">Just IntPtr.Zero, dunno</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);
        
        public delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        // Hook on window events
        /*[DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(WinEvents eventMin, WinEvents eventMax, IntPtr
   hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
   uint idThread, WinEventFlags dwFlags);*/
    }
}
