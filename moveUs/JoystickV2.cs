using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace moveUs
{
    public partial class JoystickV2 : Form
    {
        public JoystickV2()
        {
            InitializeComponent();
        }

        private void JoystickV2_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = BackColor;
        }
    }
}
