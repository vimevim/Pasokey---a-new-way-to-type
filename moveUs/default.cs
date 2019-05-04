using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace moveUs
{
    public partial class @default : Form
    {
        bool dPanelValue = false;
        bool mMenuValue = false;
        public @default()
        {
            InitializeComponent();
        }

        DualPanel dPanel = new DualPanel();

        MarkingMenu mMenu = new MarkingMenu();

        private void markingMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMenuValue == true)
            {
                mMenu.Hide();
                mMenuValue = false;
            }
            if (dPanelValue == false)
            {
                dPanel.Show();
                dPanelValue = true;
            }
            else
            {
                dPanel.Hide();
                dPanelValue = false;
            }
        }

        private void joystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dPanelValue == true)
            {
                dPanel.Hide();
                dPanelValue = false;
            }
            if (mMenuValue == false)
            {
                mMenu.Show();
                mMenuValue = true;
            }
            else
            {
                mMenu.Hide();
                mMenuValue = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
