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
    public partial class Form1 : Form
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

        int firstStep = 5;//ilk basamağımız
        int secondStep = 10;//ikinci basamağımız
        //5*8 dizi oluşturuyorum
        string[,] keyPad = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };
        bool i = false;

        private void writer(int birinciDeger, int ikinciDeger)
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
                activate(i, birinciDeger, ikinciDeger);
            }
            else
            {
                stepOne();
                activate(i, birinciDeger, ikinciDeger);
            }
        }
        private void stepOne()
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
        private void activate(bool sayac, int birinciDeger, int ikinciDeger)
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
        private void centrelize()
        {
            btn8.Left = 150;
            btn8.Top = 150;
        }
        public Form1()
        {
            InitializeComponent();
            //btn8.Left = 150;
            //btn8.Top = 150;
            //this.TransparencyKey = (BackColor);
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
        }

        public Point MouseDownLocation;
        private void ortak_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        public void button1_MouseMove(object sender, MouseEventArgs e)
        {
            int myXValue = btn8.Left - 150;//butonun değerleri x ve y ekseninde formun sol üst köşesindeki konumuna göre veriyor
            int myYValue = btn8.Top - 150;//bende bu değerleri buton üzerinden sıfır alıp görmek istediğim için buton konumunu düştüm
            double hipotenus = Math.Sqrt(myXValue * myXValue + myYValue * myYValue);// çapraz olanların merkeze uzaklığını aldık
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//butona basılı olduğu sürece çalışıyor
            {
                btn8.Left = e.X + btn8.Left - MouseDownLocation.X;
                btn8.Top = e.Y + btn8.Top - MouseDownLocation.Y;
                buttonPos.Text = ("X = " + (myXValue) + "\nY = " + (myYValue) + "\nHipotenüs = " + hipotenus);
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                centrelize();
                buttonPos.Text = ("X = " + (btn8.Left - 150) + "\nY = " + (btn8.Top - 150) + "\nHipotenüs = 0");
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
                btnSpace.Left = 115;
                btnSpace.Top = 320;
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
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//butona basılı olduğu sürece çalışıyor
            {
                btnBackSpace.Left = e.X + btnBackSpace.Left - MouseDownLocation.X;
                btnBackSpace.Top = e.Y + btnBackSpace.Top - MouseDownLocation.Y;
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                btnBackSpace.Left = 320;
                btnBackSpace.Top = 115;
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
                    writer(4, 9);
                }
                else
                {
                    stepOne();
                    i = false;
                }
            }


            else if (centreButtonY > btn1.Top && centreButtonY < (btn1.Top + 50) && centreButtonX > btn1.Left && centreButtonX < (btn1.Left + 50))
            {
                if (i == true)
                {
                    writer(1, 1);
                }
                else
                {
                    stepOne();
                    i = false;
                }
            }
            else if (centreButtonY > btn3.Top && centreButtonY < (btn3.Top + 50) && centreButtonX > btn3.Left && centreButtonX < (btn3.Left + 50))
            {
                if (i == true)
                {
                    writer(3, 3);
                }
                else
                {
                    stepOne();
                    i = false;
                }
            }
            else if (centreButtonY > btn5.Top && centreButtonY < (btn5.Top + 50) && centreButtonX > btn5.Left && centreButtonX < (btn5.Left + 50))
            {
                if (i == true)
                {
                    writer(5, 5);
                }
                else
                {
                    stepOne();
                    i = false;
                }
            }
            else if (centreButtonY > btn7.Top && centreButtonY < (btn7.Top + 50) && centreButtonX > btn7.Left && centreButtonX < (btn7.Left + 50))
            {
                if (i == true)
                {
                    writer(7, 7);
                }
                else
                {
                    stepOne();
                    i = false;
                }
            }
            else if (centreButtonY > btn6.Top && centreButtonY < (btn6.Top + 50) && centreButtonX > btn6.Left && centreButtonX < (btn6.Left + 50))
            {
                writer(3, 6);
            }
            else if (centreButtonY > btn4.Top && centreButtonY < (btn4.Top + 50) && centreButtonX > btn4.Left && centreButtonX < (btn4.Left + 50))
            {
                writer(2, 4);
            }
            else if (centreButtonY > btn2.Top && centreButtonY < (btn2.Top + 50) && centreButtonX > btn2.Left && centreButtonX < (btn2.Left + 50))
            {
                writer(1, 2);
            }
            else if (centreButtonY > btn0.Top && centreButtonY < (btn0.Top + 50) && centreButtonX > btn0.Left && centreButtonX < (btn0.Left + 50))
            {
                writer(0, 0);
            }
            else
            {
                centrelize();
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            writer(0, 0);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                writer(1, 1);
            }
            else
            {
                stepOne();
                i = false;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            writer(1, 2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                writer(3, 3);
            }
            else
            {
                stepOne();
                i = false;
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            writer(2, 4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                writer(5, 5);
            }
            else
            {
                stepOne();
                i = false;
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            writer(3, 6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (i == true)
            {
                writer(7, 7);
            }
            else
            {
                stepOne();
                i = false;
            }
        }
        //mouse konumunu veriyor
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                base.OnMouseMove(e);
                int x = e.X;
                int y = e.Y;
                mousePos.Text = "x degeri = " + x + " y degeri = " + y + " \nMouse Position = " + MousePosition.X + "\nmouse cos " + Math.Sqrt(y * y + x * x);
                /*if (e.Button == MouseButtons.Left)
                {
                    btn0.Text = ("Mouse clicked");
                }*/
            }
        }
    }
}
