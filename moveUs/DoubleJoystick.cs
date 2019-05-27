using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasoKey
{
    public partial class DoubleJoystick : Form
    {
        int leftGuideX, leftGuideY, rightGuideX, rightGuideY;
        int cursorOriginX, cursorOriginY;
        double leftHypotenuse, leftAngle, rightHypotenuse, rightAngle;
        Point mouseDownLocation;

        public DoubleJoystick()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
        }

        //Klavye kodları.
        int firstStep = 5;//ilk basamağımız
        int secondStep;//ikinci basamağımız
        string upOrLow = "low";//büyük küçük harf seçici
        //küçük harf kümesi
        string[,] keyPadLower = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };
        //büyük harf kümesi
        string[,] keyPadUpper = new string[5, 8] {
            {"A","B","C","D","D","F","G","H"},
            {"I","J","K","L","M","N","O","P"},
            {"Q","R","S","T","U","V","W","X"},
            {"Y","Z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };
        //İlk girdiye göre ikinci panele yansıtma yapıyorum.
        private void WriteFirstStepToLbl()
        {
            if (firstStep != 5)//buton hareket etse bile bir değer atandı mı emin oluyorum
            {
                if (upOrLow == "low")//büyük küçük harf kümesini seçiyorum
                {
                    rightLbl0.Text = keyPadLower[firstStep, 0];
                    rightLbl1.Text = keyPadLower[firstStep, 1];
                    rightLbl2.Text = keyPadLower[firstStep, 2];
                    rightLbl3.Text = keyPadLower[firstStep, 3];
                    rightLbl4.Text = keyPadLower[firstStep, 4];
                    rightLbl5.Text = keyPadLower[firstStep, 5];
                    rightLbl6.Text = keyPadLower[firstStep, 6];
                    rightLbl7.Text = keyPadLower[firstStep, 7];
                }
                else
                {
                    rightLbl0.Text = keyPadUpper[firstStep, 0];
                    rightLbl1.Text = keyPadUpper[firstStep, 1];
                    rightLbl2.Text = keyPadUpper[firstStep, 2];
                    rightLbl3.Text = keyPadUpper[firstStep, 3];
                    rightLbl4.Text = keyPadUpper[firstStep, 4];
                    rightLbl5.Text = keyPadUpper[firstStep, 5];
                    rightLbl6.Text = keyPadUpper[firstStep, 6];
                    rightLbl7.Text = keyPadUpper[firstStep, 7];
                }
                rightGuide.BringToFront();//panel2 yi ön plana çıkartıyorum
                general_MouseUp(null, null);//tüm butonların konumunu başlangıca çekiyorum
            }
        }
        //yansıtma yapılmış paneldeki aksiyona göre klavye girdisi veriyorum
        private void TypeTheLetter()
        {
            if (firstStep == 5)//ilk aksiyonumuz gerçekleşmediyse ikinci aksiyona geçmeyi engelliyorum
            {
                //MessageBox.Show("Lütfen ilk adımı giriniz.");
            }
            else
            {
                if (upOrLow == "low")
                {
                    SendKeys.Send(keyPadLower[firstStep, secondStep]);//klavye girdisi gönderiliyor
                    movingPartRight.Text = keyPadLower[firstStep, secondStep];//seçilen karakter hafızada buton değeri olarak tutuluyor
                }
                else
                {
                    SendKeys.Send(keyPadUpper[firstStep, secondStep]);
                    movingPartRight.Text = keyPadUpper[firstStep, secondStep];
                }
            }
            //panel1.BringToFront();
            general_MouseUp(null, null);
        }

        int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;//ekran genişliği
        int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;//ekran yüksekliği

        private void DualPanel_Load(object sender, EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width;//formun genişliğini ekranın genişliğiyle eşitleyen komut
            this.Location = new Point(screenWidth - this.Width, screenHeight - this.Height);//formun konumunu ekranın altına sabitleyen komut
            rightGuide.Location = new Point(this.Width - rightGuide.Width, 0);//panel2 konumunu başlangıçta sağa yaslayan komut.
            this.TransparencyKey = BackColor;
        }
        //butonları hareket ettirme komutları burada başlıyor

        private void leftGuide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                movingPartLeft.Left = leftGuideX;
                movingPartLeft.Top = leftGuideY;
            }
        }

        private void rightGuide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                movingPartRight.Left = rightGuideX;
                movingPartRight.Top = rightGuideY;
            }
        }

        private void leftGuide_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftHypotenuse <= 50)
            {
                firstStep = 4;
            }
            else
            {
                if (leftAngle > 337.5 || leftAngle < 22.5)
                {
                    firstStep = 2;
                }
                else if (leftAngle > 22.5 && leftAngle < 67.5)
                {

                    if (upOrLow == "low")
                    {
                        upOrLow = "up";
                    }
                    else
                    {
                        upOrLow = "low";
                    }
                    WriteFirstStepToLbl();
                }
                else if (leftAngle > 67.5 && leftAngle < 112.5)
                {
                    firstStep = 3;
                }
                else if (leftAngle > 112.5 && leftAngle < 157.5)
                {
                    SendKeys.Send(" ");
                }
                else if (leftAngle > 157.5 && leftAngle < 202.5)
                {
                    firstStep = 0;
                }
                else if (leftAngle > 202.5 && leftAngle < 247.5)
                {
                    SendKeys.Send("{BS}");
                }
                else if (leftAngle > 247.5 && leftAngle < 292.5)
                {
                    firstStep = 1;
                }
                else if (leftAngle > 292.5 && leftAngle < 337.5)
                {
                    SendKeys.Send("{ENTER}");
                }
                else
                {//gereksiz ama olsun :D 
                    general_MouseUp(null, null);
                }
            }
            WriteFirstStepToLbl();
        }

        private void rightGuide_MouseUp(object sender, MouseEventArgs e)
        {
            if (rightHypotenuse <= 50)
            {
                leftGuide.BringToFront();
            }
            else
            {
                if (rightAngle > 337.5 || rightAngle < 22.5)
                {
                    secondStep = 4;
                }
                else if (rightAngle > 22.5 && rightAngle < 67.5)
                {
                    secondStep = 5;
                }
                else if (rightAngle > 67.5 && rightAngle < 112.5)
                {
                    secondStep = 6;
                }
                else if (rightAngle > 112.5 && rightAngle < 157.5)
                {
                    secondStep = 7;
                }
                else if (rightAngle > 157.5 && rightAngle < 202.5)
                {
                    secondStep = 0;
                }
                else if (rightAngle > 202.5 && rightAngle < 247.5)
                {
                    secondStep = 1;
                }
                else if (rightAngle > 247.5 && rightAngle < 292.5)
                {
                    secondStep = 2;
                }
                else if (rightAngle > 292.5 && rightAngle < 337.5)
                {
                    secondStep = 3;
                }
                else
                {
                    general_MouseUp(null, null);
                }
                TypeTheLetter();
            }
        }

        private void leftGuide_MouseMove(object sender, MouseEventArgs e)
        {
            //orijine göre leftHypotenuse hesaplaması
            leftHypotenuse = Math.Sqrt(cursorOriginX * cursorOriginX + cursorOriginY * cursorOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            leftAngle = Math.Acos(cursorOriginY / leftHypotenuse);
            if (cursorOriginX < 0)
            {
                leftAngle = leftAngle * 180 / Math.PI;
            }
            else
            {
                leftAngle -= leftAngle * 180 / Math.PI;
                leftAngle += 360;
            }

            leftGuideX = e.X - (movingPartLeft.Width / 2);
            leftGuideY = e.Y - (movingPartLeft.Height/2);
            cursorOriginX = e.X - (leftGuide.Width / 2);
            cursorOriginY = e.Y - (leftGuide.Height / 2);

            //if the cursor moves on top of the button
            if (e.Button == MouseButtons.Left)//if the button click continous
            {
                if (leftHypotenuse < 125)//daire içerisinden çıkartmıyor
                {
                    if (cursorOriginX < 125 && cursorOriginX > -125)//x ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        movingPartLeft.Left = leftGuideX;
                    }
                    if (cursorOriginY < 125 && cursorOriginY > -125)//y ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        movingPartLeft.Top = leftGuideY;
                    }
                }
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                general_MouseUp(null, null);
            }
        }
        private void rightGuide_MouseMove(object sender, MouseEventArgs e)
        {
            //orijine göre leftHypotenuse hesaplaması
            rightHypotenuse = Math.Sqrt(cursorOriginX * cursorOriginX + cursorOriginY * cursorOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            rightAngle = Math.Acos(cursorOriginY / rightHypotenuse);
            if (cursorOriginX < 0)
            {
                rightAngle = rightAngle * 180 / Math.PI;
            }
            else
            {
                rightAngle -= rightAngle * 180 / Math.PI;
                rightAngle += 360;
            }

            rightGuideX = e.X - (movingPartRight.Width / 2);
            rightGuideY = e.Y - (movingPartRight.Height / 2);
            cursorOriginX = e.X - (rightGuide.Width / 2);
            cursorOriginY = e.Y - (rightGuide.Height / 2);


            //if the cursor moves on top of the button
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if the button click continous
            {
                if (rightHypotenuse < 125)//daire içerisinden çıkartmıyor
                {
                    if (cursorOriginX < 125 && cursorOriginX > -125)//x ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        movingPartRight.Left = rightGuideX;
                    }
                    if (cursorOriginY < 125 && cursorOriginY > -125)//y ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        movingPartRight.Top = rightGuideY;
                    }
                }
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                general_MouseUp(null, null);
            }
        }

        //buton hareketleri için olan komutlar burada bitiyor

        private void general_MouseUp(object sender, MouseEventArgs e)
        {
            movingPartLeft.Left = 150;
            movingPartLeft.Top = 150;
            movingPartRight.Left = 150;
            movingPartRight.Top = 150;
        }
    }
}
