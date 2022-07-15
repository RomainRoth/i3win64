using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i3win64
{
    internal class HWindow
    {
        private IntPtr hWnd;

        public HWindow(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }
    }
}
