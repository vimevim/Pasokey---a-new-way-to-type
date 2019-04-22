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
    public partial class RealJoystick : Form
    {
        int btn8X;
        int btn8Y;
        int cursorOriginX, cursorOriginY;
        double hypotenuse, angle;

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
        bool i = false; //değişken parametre oluşturuldu ve false olarak atandı
        //5*8 dizi oluşturuyorum
        string[,] keyPad = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };

        private void WriteFirstStepToLbl(int birinciDeger, int ikinciDeger)
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

        public RealJoystick()
        {
            InitializeComponent();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - this.Width, workingArea.Bottom - this.Height);
        }

        private void switchForm_Click(object sender, EventArgs e)
        {
            SingleForm frm1 = new SingleForm();
            frm1.Show();
            this.Hide();
        }

        public Point MouseDownLocation;//genel amaçlı point değişkeni oluşturuldu ve mouseDown olduğundaki konumu atandı

        private void ortak_MouseDown(object sender, MouseEventArgs e)
        {//butona basıldığı anda çalışan komut, basılıp çekince değil basılır basılmaz çalışır.
            MouseDownLocation = e.Location;
        }

        private void btn8_MouseMove(object sender, MouseEventArgs e)
        {
            //orijine göre hypotenuse hesaplaması
            hypotenuse = Math.Sqrt(cursorOriginX * cursorOriginX + cursorOriginY * cursorOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            angle = Math.Acos(cursorOriginY / hypotenuse);
            if (cursorOriginX < 0)
            {
                angle = angle * 180 / Math.PI;
            }
            else
            {
                angle -= angle * 180 / Math.PI;
                angle += 360;
            }

            btn8X = e.X + btn8.Left - MouseDownLocation.X;
            btn8Y = e.Y + btn8.Top - MouseDownLocation.Y;
            cursorOriginX = btn8X + 25 - (this.Width / 2);
            cursorOriginY = btn8Y + 25 - (this.Height / 2);

            //if the cursor moves on top of the button
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if the button click continous
            {
                if (hypotenuse < 100)//daire içerisinden çıkartmıyor
                {
                    if (cursorOriginX < 100 && cursorOriginX > -100)//x ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        btn8.Left = btn8X;
                    }
                    if (cursorOriginY < 100 && cursorOriginY > -100)//y ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        btn8.Top = btn8Y;
                    }
                }
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                ortak_MouseUp(null, null);
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (hypotenuse <= 50)
            {
                if (i == false)
                {
                    WriteFirstStepToLbl(4, 9);
                }
                else
                {
                    ResetText();
                    i = false;
                }
            }
            else
            {
                if (angle > 337.5 || angle < 22.5)
                {
                    WriteFirstStepToLbl(2, 4);
                }
                else if (angle > 22.5 && angle < 67.5)
                {
                    if (i == true)
                    {
                        WriteFirstStepToLbl(5, 5);
                    }
                    else
                    {
                        ResetText();
                        i = false;
                    }
                }
                else if (angle > 67.5 && angle < 112.5)
                {
                    WriteFirstStepToLbl(3, 6);
                }
                else if (angle > 112.5 && angle < 157.5)
                {
                    if (i == true)
                    {
                        WriteFirstStepToLbl(7, 7);
                    }
                    else
                    {
                        ResetText();
                        i = false;
                    }
                }
                else if (angle > 157.5 && angle < 202.5)
                {
                    WriteFirstStepToLbl(0, 0);
                }
                else if (angle > 202.5 && angle < 247.5)
                {
                    if (i == true)
                    {
                        WriteFirstStepToLbl(1, 1);
                    }
                    else
                    {
                        ResetText();
                        i = false;
                    }
                }
                else if (angle > 247.5 && angle < 292.5)
                {
                    WriteFirstStepToLbl(1, 2);
                }
                else if (angle > 292.5 && angle < 337.5)
                {
                    if (i == true)
                    {
                        WriteFirstStepToLbl(3, 3);
                    }
                    else
                    {
                        ResetText();
                        i = false;
                    }
                }
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
    }
}
