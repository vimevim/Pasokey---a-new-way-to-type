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
    public partial class panelRight : Form
    {
        public panelRight()
        {
            InitializeComponent();
        }

        private void panelRight_Load(object sender, EventArgs e)
        {
            this.Width = 5;
            this.Height = 400;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
        }


        Point birinciDeger, ikinciDeger, sonDeger;

        private void panelRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = (e.Y + this.Top - mouseDownLocation.Y);
            }
            birinciDeger = new Point(e.X, e.Y);
        }

        Point mouseDownLocation;

        private void panelRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
            if (this.Width == 5)
            {
                kepenkAc.Start();
                sleepModeActivate.Start();
            }
        }

        private void kepenkAc_Tick(object sender, EventArgs e)
        {
            this.Width += 5;
            this.Left -= 5;
            if (this.Width == 150)
            {
                kepenkAc.Stop();
            }
        }

        private void kepenkKapat_Tick(object sender, EventArgs e)
        {
            this.Width -= 5;
            this.Left += 5;
            if (this.Width == 5)
            {
                kepenkKapat.Stop();
            }
        }

        private void sleepModeActivate_Tick(object sender, EventArgs e)
        {
            sonDeger = ikinciDeger;
            ikinciDeger = birinciDeger;
            if (birinciDeger == sonDeger)
            {
                if (this.Width == 150)
                {
                    kepenkKapat.Start();
                }
            }
        }
    }
}
