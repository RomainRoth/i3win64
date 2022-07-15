using System;
using System.Windows.Forms;

namespace i3win64
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new ZPool());
        }
    }
}
