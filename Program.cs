using System;
using System.Windows.Forms;

namespace i3win64
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ZPool());
        }
    }
}
