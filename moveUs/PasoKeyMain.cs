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

namespace moveUs
{
    public partial class PasoKeyMain : Form
    {

        DoubleJoystick doubleJoystick = new DoubleJoystick();

        SingleJoystick singleJoystick = new SingleJoystick();

        FloatingJoystick floatingJoystick = new FloatingJoystick();

        FloatingMarkingMenu floatingMarkingMenu = new FloatingMarkingMenu();

        FixedMarkingMenu fixedMarkingMenu = new FixedMarkingMenu();

        PanelRight panelRight = new PanelRight();

        PanelTop panelTop = new PanelTop();

        private void allHide()
        {
            doubleJoystick.Hide();
            singleJoystick.Hide();
            floatingJoystick.Hide();
            floatingMarkingMenu.Hide();
            fixedMarkingMenu.Hide();
        }

        public PasoKeyMain()
        {
            InitializeComponent();
            panelRight.Show();
            panelTop.Show();
            Hook.GlobalEvents().MouseClick += async (sender, e) =>
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
            };
        }

        private void PasoKeyMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (key.GetValue("PasoKey").ToString() == "\"" + Application.ExecutablePath + "\"")
                { // Eğer regeditte varsa, checkbox ı işaretle
                    ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = true; //false;
                }
            }
            catch
            {

            }
        }


        private void runAtStartUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked == false)
            { //işaretlendi ise Regedit e açılışta çalıştır olarak ekle
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("PasoKey", "\"" + Application.ExecutablePath + "\"");
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = true; //false;
            }
            else
            {  //işaret kaldırıldı ise Regeditten açılışta çalıştırılacaklardan kaldır
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("PasoKey");
                ((ToolStripMenuItem)contextMenuStrip1.Items[8]).Checked = false; //false;
            }
        }

        private void closeThePasoKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void markingMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
