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
    public partial class ZTile : UserControl
    {
        #region Attributes
        private IntPtr hWdn;
        public IntPtr HWdn { get => hWdn; set => hWdn = value; }

        #endregion

        #region Methods
        
        #endregion

        public ZTile(IntPtr _hWdn)
        {
            hWdn = _hWdn;
            InitializeComponent();
        }

        private void ZTile_Load(object sender, EventArgs e)
        {
            hWdn.AttachTo(panelHolder);
        }
    }
}
