using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i3win64
{
    public partial class ZScreen : Form
    {
        public ZScreen()
        {
            InitializeComponent();
        }

        public ZTile? FindZTile(IntPtr hWdn)
        {
            return Controls.OfType<ZTile>().FirstOrDefault(x => x.HWdn == hWdn);
        }

        public void GrowW(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if(t!=null)
            {
                t.Width += 10;
            }   
        }

        public void ShrinkW(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Width -= 10;
            }
        }

        public void GrowH(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Height += 10;
            }
        }

        public void ShrinkH(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Height -= 10;
            }
        }

        public void MoveR(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Left += 10;
            }
        }

        public void MoveL(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Left -= 10;
            }
        }

        public void MoveD(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Top += 10;
            }
        }

        public void MoveU(IntPtr hWdn)
        {
            ZTile? t = FindZTile(hWdn);
            if (t != null)
            {
                t.Top -= 10;
            }
        }
    }
}
