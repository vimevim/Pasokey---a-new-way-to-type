using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace moveUs
{
    public partial class PasoKeyMain : Form
    {
        bool dPanelValue = false;
        bool mMenuValue = false;
        public PasoKeyMain()
        {
            InitializeComponent();
        }



        private void PasoKeyMain_Load(object sender, EventArgs e)
        {
            this.Hide();
        }


        Joystick joystick = new Joystick();

        MarkingMenu markingMenu = new MarkingMenu();

        private void joystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMenuValue == true)
            {
                markingMenu.Hide();
                mMenuValue = false;
            }
            if (dPanelValue == false)
            {
                joystick.Show();
                dPanelValue = true;
            }
            else
            {
                joystick.Hide();
                dPanelValue = false;
            }
        }

        private void markingMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dPanelValue == true)
            {
                joystick.Hide();
                dPanelValue = false;
            }
            if (mMenuValue == false)
            {
                markingMenu.Show();
                mMenuValue = true;
            }
            else
            {
                markingMenu.Hide();
                mMenuValue = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            /*if (dPanelValue == false)
            {
                dPanel.Show();
                dPanelValue = true;
            }
            else
            {
                dPanel.Hide();
                if (mMenuValue == false)
                {
                    markingMenu.Show();
                    mMenuValue = true;
                }
                else
                {
                    markingMenu.Hide();
                    mMenuValue = false;
                    dPanel.Hide();
                    dPanelValue = false;
                }
            }*/
        }


    }
}
