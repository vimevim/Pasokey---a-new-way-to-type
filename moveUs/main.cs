using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gma.System.Windows;

namespace moveUs
{
    public partial class main : Form
    {
        bool dPanelValue = false;
        bool mMenuValue = false;
        public main()
        {
            InitializeComponent();
        }

        UserActivityHook actHook;

        private void main_Load(object sender, EventArgs e)
        {
            this.Hide();
            actHook = new UserActivityHook(); // crate an instance with global hooks
                                              // hang on events
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.Start();
        }
        public void MouseMoved(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.Show();
                    this.TopMost = true;
                    this.Location = new Point(MousePosition.X, MousePosition.Y);

                    /*contextMenuStrip1.Show();
                    contextMenuStrip1.Left = MousePosition.X;
                    contextMenuStrip1.Top = MousePosition.Y;*/
                }
                else
                {
                    this.Hide();
                }
            }
        }
        Joystick joystick = new Joystick();

        MarkingMenu mMenu = new MarkingMenu();

        private void joystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMenuValue == true)
            {
                mMenu.Hide();
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
                    mMenu.Show();
                    mMenuValue = true;
                }
                else
                {
                    mMenu.Hide();
                    mMenuValue = false;
                    dPanel.Hide();
                    dPanelValue = false;
                }
            }*/
        }
    }
}
