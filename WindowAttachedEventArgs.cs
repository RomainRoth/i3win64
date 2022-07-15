using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i3win64
{
    internal class WindowAttachedEventArgs : EventArgs
    {
        public IntPtr WindowHandle { get; private set; }

        public WindowAttachedEventArgs(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }
    }
}
