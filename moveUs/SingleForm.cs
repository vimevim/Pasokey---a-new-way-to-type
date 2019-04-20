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
    public partial class SingleForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
        }//Klavye kodları.

        int firstStep = 5, secondStep = 10;//ikinci basamağımız
        bool i = false; //değişken parametre oluşturuldu ve false olarak atandı
        //5*8 dizi oluşturuyorum
        string[,] keyPad = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };

        private void Writer(int birinciDeger, int ikinciDeger)
        {//burada gerçekten iyi bir şey kullan ve bu kod hammallığından kurtul.
            if (i == false)
            {
                btn0.Text = keyPad[birinciDeger, 0];
                btn1.Text = keyPad[birinciDeger, 1];
                btn2.Text = keyPad[birinciDeger, 2];
                btn3.Text = keyPad[birinciDeger, 3];
                btn4.Text = keyPad[birinciDeger, 4];
                btn5.Text = keyPad[birinciDeger, 5];
                btn6.Text = keyPad[birinciDeger, 6];
                btn7.Text = keyPad[birinciDeger, 7];
                btn8.Text = "X";
            }
            else
            {
                ResetText();
            }
            TypeTheLetter(i, birinciDeger, ikinciDeger);
        }
        private void ResetText()
        {
            btn0.Text = "X";
            btn1.Text = "X";
            btn2.Text = "X";
            btn3.Text = "X";
            btn4.Text = "X";
            btn5.Text = "X";
            btn6.Text = "X";
            btn7.Text = "X";
            btn8.Text = "<";
        }
        private void TypeTheLetter(bool sayac, int birinciDeger, int ikinciDeger)
        {
            if (sayac == false)
            {
                firstStep = birinciDeger;
                i = true;
            }
            else
            {
                secondStep = ikinciDeger;
                SendKeys.Send(keyPad[firstStep, secondStep]);
                i = false;
            }
        }

        public SingleForm()
        {
            InitializeComponent();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - this.Width, workingArea.Bottom - this.Height);
        }

        public Point MouseDownLocation;//genel amaçlı point değişkeni oluşturuldu ve mouseDown olduğundaki konumu atandı

        private void ortak_MouseDown(object sender, MouseEventArgs e)
        {//butona basıldığı anda çalışan komut, basılıp çekince değil basılır basılmaz çalışır.
            MouseDownLocation = e.Location;
        }

        private void btn8_MouseMove(object sender, MouseEventArgs e)
        {//if the cursor moves on top of the button
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if the button click continous
            {
                btn8.Left = e.X + btn8.Left - MouseDownLocation.X;
                btn8.Top = e.Y + btn8.Top - MouseDownLocation.Y;
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                ortak_MouseUp(null, null);
            }
        }

        private void btnSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//butona basılı olduğu sürece çalışıyor
            {
                btnSpace.Left = e.X + btnSpace.Left - MouseDownLocation.X;
                btnSpace.Top = e.Y + btnSpace.Top - MouseDownLocation.Y;
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                ortak_MouseUp(null, null);
            }
        }
        private void btnSpace_Click(object sender, EventArgs e)
        {
            if (btnSpace.Top <= 300)
            {
                SendKeys.Send(" ");
            }
            else if (btnSpace.Top >= 320 && btnSpace.Top <= 360 && btnSpace.Left >= 15 && btnSpace.Left <= 235)
            {
                SendKeys.Send(" ");
            }
        }

        private void btnBackSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                btnBackSpace.Left = e.X + btnBackSpace.Left - MouseDownLocation.X;
                btnBackSpace.Top = e.Y + btnBackSpace.Top - MouseDownLocation.Y;
            }
            else
            {
                ortak_MouseUp(null, null);
            }
        }
        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (btnBackSpace.Left <= 300)
            {
                SendKeys.Send("{BS}");
            }
            else if (btnBackSpace.Left >= 320 && btnBackSpace.Left <= 360 && btnBackSpace.Top >= 15 && btnBackSpace.Top <= 235)
            {
                SendKeys.Send("{BS}");
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            int centreButtonX = btn8.Left + 25;//left değeri bize butonun merkezinden değil sol başlangıcından değer veriyor 
            int centreButtonY = btn8.Top + 25;// top ta öyle, bende butonun yarısı kadar değer verip butonun merkezini simüle ediyorum.
            if (btn8.Top >= 150 && btn8.Top <= 200 && btn8.Left >= 150 && btn8.Left <= 200)
            {
                if (i == false)
                {
                    Writer(4, 9);
                }
                else
                {
                    ResetText();
                    i = false;
                }
            }
            else if (centreButtonY > btn1.Top && centreButtonY < (btn1.Top + 50) && centreButtonX > btn1.Left && centreButtonX < (btn1.Left + 50))
            {
                btn1_Click(null, null);
            }
            else if (centreButtonY > btn3.Top && centreButtonY < (btn3.Top + 50) && centreButtonX > btn3.Left && centreButtonX < (btn3.Left + 50))
            {
                btn3_Click(null, null);
            }
            else if (centreButtonY > btn5.Top && centreButtonY < (btn5.Top + 50) && centreButtonX > btn5.Left && centreButtonX < (btn5.Left + 50))
            {
                btn5_Click(null, null);
            }
            else if (centreButtonY > btn7.Top && centreButtonY < (btn7.Top + 50) && centreButtonX > btn7.Left && centreButtonX < (btn7.Left + 50))
            {
                btn7_Click(null, null);
            }
            else if (centreButtonY > btn6.Top && centreButtonY < (btn6.Top + 50) && centreButtonX > btn6.Left && centreButtonX < (btn6.Left + 50))
            {
                btn6_Click(null, null);
            }
            else if (centreButtonY > btn4.Top && centreButtonY < (btn4.Top + 50) && centreButtonX > btn4.Left && centreButtonX < (btn4.Left + 50))
            {
                btn4_Click(null, null);
            }
            else if (centreButtonY > btn2.Top && centreButtonY < (btn2.Top + 50) && centreButtonX > btn2.Left && centreButtonX < (btn2.Left + 50))
            {
                btn2_Click(null, null);
            }
            else if (centreButtonY > btn0.Top && centreButtonY < (btn0.Top + 50) && centreButtonX > btn0.Left && centreButtonX < (btn0.Left + 50))
            {
                btn0_Click(null, null);
            }
            else
            {
                ortak_MouseUp(null, null);
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            Writer(0, 0);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                Writer(1, 1);
            }
            else
            {
                ResetText();
                i = false;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Writer(1, 2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                Writer(3, 3);
            }
            else
            {
                ResetText();
                i = false;
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Writer(2, 4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                Writer(5, 5);
            }
            else
            {
                ResetText();
                i = false;
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Writer(3, 6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                Writer(7, 7);
            }
            else
            {
                ResetText();
                i = false;
            }
        }
        private void ortak_MouseUp(object sender, MouseEventArgs e)
        {
            btn8.Left = 150;
            btn8.Top = 150;
            btnBackSpace.Left = 320;
            btnBackSpace.Top = 115;
            btnSpace.Left = 115;
            btnSpace.Top = 320;
        }

        private void switchForm_Click(object sender, EventArgs e)
        {
            RealJoystick js = new RealJoystick();
            js.Show();
            this.Hide();
        }
    }
}
