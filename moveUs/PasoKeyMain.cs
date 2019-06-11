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
using Gma.System.MouseKeyHook;

namespace PasoKey
{
    public partial class PasoKeyMain : Form
    {


        Joystick joystick = new Joystick();

        GamepadMod gamepadMod = new GamepadMod();

        MarkingMenu markingMenu = new MarkingMenu();

        Panel panel = new Panel();
        public void allHideAndFalse()
        {
            markingMenu.Hide();
            joystick.Hide();
            ((ToolStripMenuItem)contextMenuStrip1.Items[0]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[1]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[3]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[4]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[5]).Checked = false;
        }

        public PasoKeyMain()
        {
            InitializeComponent();
            if (ayarlar.Default.panelMod == "left")
            {
                ((ToolStripMenuItem)contextMenuStrip1.Items[7]).Checked = true;
                panel.Show();
            }
            else if (ayarlar.Default.panelMod == "top")
            {
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = true;
                panel.Show();
            }
            else if (ayarlar.Default.panelMod == "right")
            {
                ((ToolStripMenuItem)contextMenuStrip1.Items[9]).Checked = true;
                panel.Show();
            }
            else if (ayarlar.Default.panelMod == "non")
            {
            }
            Hook.GlobalEvents().MouseClick += async (sender, e) =>
            {
                if (ayarlar.Default.midBut == true)
                {
                    /*
                     * if (e.Button == MouseButtons.Middle)
                    {
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        this.Left = Cursor.Position.X;
                        this.Top = Cursor.Position.Y;
                    }
                    else
                    {
                        if (this.WindowState == FormWindowState.Normal)//form minimize değilse çalıştır
                        {//form normal ise burası çalışır, her seferinde çalışmasını engelleyen bir sınırlayıcı
                            //mouse, form kordinatları dışındaysa çalışır
                            if (Cursor.Position.X < this.Left || Cursor.Position.X > (this.Left + this.Width) || Cursor.Position.Y < this.Top || Cursor.Position.Y > (this.Top + this.Height))
                            {
                                this.Hide();
                                this.WindowState = FormWindowState.Minimized;//formu minimize et
                            }
                        }

                    }
                    */
                    if (e.Button == MouseButtons.Middle)
                    {
                        contextMenuStrip1.Show();
                        contextMenuStrip1.Left = Cursor.Position.X;
                        contextMenuStrip1.Top = Cursor.Position.Y;
                    }
                    else
                    {
                        //mouse, form kordinatları dışındaysa çalışır
                        if (Cursor.Position.X < contextMenuStrip1.Left || Cursor.Position.X > (contextMenuStrip1.Left + contextMenuStrip1.Width) || Cursor.Position.Y < contextMenuStrip1.Top || Cursor.Position.Y > (contextMenuStrip1.Top + contextMenuStrip1.Height))
                        {
                            contextMenuStrip1.Hide();
                        }
                    }
                }
            };
        }

        private void PasoKeyMain_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void floatingMarkingMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)contextMenuStrip1.Items[0]).Checked == false)
            {
                allHideAndFalse();
                markingMenu.Show();
                ((ToolStripMenuItem)contextMenuStrip1.Items[0]).Checked = true;
                ayarlar.Default.theMod = "FloatingMarkingMenu";
                ayarlar.Default.Save();
                markingMenu.Controls.Clear();
                markingMenu.InitializeComponent();
                markingMenu.MarkingMenu_Load(null, null);
            }
            else
            {
                allHideAndFalse();
            }
        }

        private void fixedMarkingMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)contextMenuStrip1.Items[1]).Checked == false)
            {
                allHideAndFalse();
                markingMenu.Show();
                ((ToolStripMenuItem)contextMenuStrip1.Items[1]).Checked = true;
                ayarlar.Default.theMod = "FixedMarkingMenu";
                ayarlar.Default.Save();
                markingMenu.Controls.Clear();
                markingMenu.InitializeComponent();
                markingMenu.MarkingMenu_Load(null, null);
            }
            else
            {
                allHideAndFalse();
            }
        }

        private void doubleJoystickToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (((ToolStripMenuItem)contextMenuStrip1.Items[3]).Checked == false)
            {
                allHideAndFalse();
                joystick.Show();
                ((ToolStripMenuItem)contextMenuStrip1.Items[3]).Checked = true;
                ayarlar.Default.theMod = "DoubleJoystick";
                ayarlar.Default.Save();
                joystick.Controls.Clear();
                joystick.InitializeComponent();
                joystick.joystickLoad(null, null);
            }
            else
            {
                allHideAndFalse();
            }
        }

        private void singleJoystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)contextMenuStrip1.Items[4]).Checked == false)
            {
                allHideAndFalse();
                joystick.Show();
                ((ToolStripMenuItem)contextMenuStrip1.Items[4]).Checked = true;
                ayarlar.Default.theMod = "SingleJoystick";
                ayarlar.Default.Save();
                joystick.Controls.Clear();
                joystick.InitializeComponent();
                joystick.joystickLoad(null, null);
            }
            else
            {
                allHideAndFalse();
            }
        }

        private void floatingJoystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)contextMenuStrip1.Items[5]).Checked == false)
            {
                allHideAndFalse();
                joystick.Show();
                ((ToolStripMenuItem)contextMenuStrip1.Items[5]).Checked = true;
                ayarlar.Default.theMod = "FloatingJoystick";
                ayarlar.Default.Save();
                joystick.Controls.Clear();
                joystick.InitializeComponent();
                joystick.joystickLoad(null, null);
            }
            else
            {
                allHideAndFalse();
            }
        }

        private void quickPanelLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel.Show();
            if (ayarlar.Default.panelMod != "left")
            {
                ayarlar.Default.panelMod = "left";
                ((ToolStripMenuItem)contextMenuStrip1.Items[7]).Checked = true;
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = false;
                ((ToolStripMenuItem)contextMenuStrip1.Items[9]).Checked = false;
                ayarlar.Default.Save();
                panel.Changes();
            }
            else if (ayarlar.Default.panelMod == "left")
            {
                panelHide();

            }
        }

        private void quickPanelTopToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel.Show();
            if (ayarlar.Default.panelMod != "top")
            {
                ayarlar.Default.panelMod = "top";
                ((ToolStripMenuItem)contextMenuStrip1.Items[7]).Checked = false;
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = true;
                ((ToolStripMenuItem)contextMenuStrip1.Items[9]).Checked = false;
                ayarlar.Default.Save();
                panel.Changes();
            }
            else if (ayarlar.Default.panelMod == "top")
            {
                panelHide();
            }
        }

        private void quickPanelRightToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panel.Show();
            if (ayarlar.Default.panelMod != "right")
            {

                ayarlar.Default.panelMod = "right";
                ((ToolStripMenuItem)contextMenuStrip1.Items[7]).Checked = false;
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = false;
                ((ToolStripMenuItem)contextMenuStrip1.Items[9]).Checked = true;
                ayarlar.Default.Save();
                panel.Changes();
            }
            else if (ayarlar.Default.panelMod == "right")
            {
                panelHide();
            }
        }
        private void panelHide()
        {
            ayarlar.Default.panelMod = "non";
            ayarlar.Default.Save();
            ((ToolStripMenuItem)contextMenuStrip1.Items[9]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = false;
            ((ToolStripMenuItem)contextMenuStrip1.Items[7]).Checked = false;
            panel.Hide();
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
        private void closeThePasoKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            ayarlar.Default.Save();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //allHideAndFalse();
            ayarlar.Default.theMod = ayarlar.Default.notifyiIconClick;
            if (ayarlar.Default.notifyiIconClick == "FloatingJoystick")
            {
                floatingJoystickToolStripMenuItem_Click(null, null);
            }
            else if (ayarlar.Default.notifyiIconClick == "SingleJoystick")
            {
                singleJoystickToolStripMenuItem_Click(null, null);
            }
            else if (ayarlar.Default.notifyiIconClick == "DoubleJoystick")
            {
                doubleJoystickToolStripMenuItem_Click(null, null);
            }
            else if (ayarlar.Default.notifyiIconClick == "FloatingMarkingMenu")
            {
                floatingMarkingMenuToolStripMenuItem_Click(null, null);
            }
            else if (ayarlar.Default.notifyiIconClick == "FixedMarkingMenu")
            {
                fixedMarkingMenuToolStripMenuItem_Click(null, null);
            }
        }

        private void gamePadModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gamepadMod.Show();
        }
    }
}
