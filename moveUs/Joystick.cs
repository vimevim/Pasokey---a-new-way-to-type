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
    public partial class Joystick : Form
    {
        int leftGuideX, leftGuideY, rightGuideX, rightGuideY;
        int cursorOriginX, cursorOriginY;
        double leftHypotenuse, leftAngle, rightHypotenuse, rightAngle;
        Point mouseDownLocation;

        public Joystick()
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
        //ortak sınıfı çağırıyoruz
        ortakSinif ortaksinif = new ortakSinif();

        //Klavye kodları.
        int firstStep = 5;//ilk basamağımız
        int secondStep;//ikinci basamağımız
        string upOrLow = "low";//büyük küçük harf seçici
        //küçük harf kümesi
        string[,] keyPad = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };
        //büyük harf kümesi
        //İlk girdiye göre ikinci panele yansıtma yapıyorum.
        private void WriteFirstStepToLbl()
        {
            if (firstStep != 5)//buton hareket etse bile bir değer atandı mı emin oluyorum
            {
                if (upOrLow == "low")//büyük küçük harf kümesini seçiyorum
                {
                    rightLbl0.Text = keyPad[firstStep, 0];
                    rightLbl1.Text = keyPad[firstStep, 1];
                    rightLbl2.Text = keyPad[firstStep, 2];
                    rightLbl3.Text = keyPad[firstStep, 3];
                    rightLbl4.Text = keyPad[firstStep, 4];
                    rightLbl5.Text = keyPad[firstStep, 5];
                    rightLbl6.Text = keyPad[firstStep, 6];
                    rightLbl7.Text = keyPad[firstStep, 7];
                }
                else
                {
                    rightLbl0.Text = keyPad[firstStep, 0].ToUpper();
                    rightLbl1.Text = keyPad[firstStep, 1].ToUpper();
                    rightLbl2.Text = keyPad[firstStep, 2].ToUpper();
                    rightLbl3.Text = keyPad[firstStep, 3].ToUpper();
                    rightLbl4.Text = keyPad[firstStep, 4].ToUpper();
                    rightLbl5.Text = keyPad[firstStep, 5].ToUpper();
                    rightLbl6.Text = keyPad[firstStep, 6].ToUpper();
                    rightLbl7.Text = keyPad[firstStep, 7].ToUpper();
                }
                rightGuide.BringToFront();//panel2 yi ön plana çıkartıyorum
                general_MouseUp(null, null);//tüm butonların konumunu başlangıca çekiyorum
            }
        }
        //yansıtma yapılmış paneldeki aksiyona göre klavye girdisi veriyorum

        public void joystickLoad(object sender, EventArgs e)
        {
            Changes();
        }

        public void Changes()
        {
            if (ayarlar.Default.theMod == "SingleJoystick")
            {
                panelGuider.Visible = false;
                this.Width = leftGuide.Width;//formun boyutu bir panel boyutuyla eşitlenir
                this.Height = leftGuide.Height;
                leftGuide.Top = 0;
                rightGuide.Top = 0;
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);//formun konumu sağ alta sabitlenir
                rightGuide.Location = new Point(0, 0);//panel2 konumu panel1 ile üst üste getirilir
            }
            else if (ayarlar.Default.theMod == "DoubleJoystick")
            {
                panelGuider.Visible = false;
                this.Height = leftGuide.Height;
                this.Width = Screen.PrimaryScreen.Bounds.Width;//formun genişliğini ekranın genişliğiyle eşitleyen komut
                leftGuide.Top = 0;
                rightGuide.Top = 0;
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);//formun konumunu ekranın altına sabitleyen komut
                rightGuide.Left = this.Width - rightGuide.Width;//panel2 konumunu başlangıçta sağa yaslayan komut.

            }
            else if (ayarlar.Default.theMod == "FloatingJoystick")
            {
                this.Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Width / 2);
                this.Top = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Height / 2);
                panelGuider.Visible = true;
                this.Width = leftGuide.Width;//formun boyutu bir panel boyutuyla eşitlenir
                this.Height = 400;
                rightGuide.Location = new Point(0, 50);//panel2 konumu panel1 ile üst üste getirilir
                leftGuide.Location = new Point(0, 50);//panel2 konumu panel1 ile üst üste getirilir
            }
            this.TransparencyKey = BackColor;
            leftGuide.BringToFront();
        }

        //move it down
        int panelGuiderLeft, panelGuiderTop;
        private void panelGuider_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panelGuiderLeft = e.X;
                panelGuiderTop = e.Y;
            }
        }
        private void panelGuider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = Cursor.Position.X - panelGuiderLeft;
                this.Top = Cursor.Position.Y - panelGuiderTop;
            }
        }


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
                WriteFirstStepToLbl();
            }
            else
            {
                if (leftAngle > 337.5 || leftAngle < 22.5)
                {
                    firstStep = 2;
                    WriteFirstStepToLbl();
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
                    WriteFirstStepToLbl();
                }
                else if (leftAngle > 112.5 && leftAngle < 157.5)
                {
                    SendKeys.Send(" ");
                }
                else if (leftAngle > 157.5 && leftAngle < 202.5)
                {
                    firstStep = 0;
                    WriteFirstStepToLbl();
                }
                else if (leftAngle > 202.5 && leftAngle < 247.5)
                {
                    SendKeys.Send("{BS}");
                }
                else if (leftAngle > 247.5 && leftAngle < 292.5)
                {
                    firstStep = 1;
                    WriteFirstStepToLbl();
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
        }

        private void rightGuide_MouseUp(object sender, MouseEventArgs e)
        {
            if (rightHypotenuse <= 50)
            {
                leftGuide.BringToFront();
            }
            else
            {
                //secondStep değişkenini decSecondStep methoduna rightAngle açısı vererek atadık
                secondStep = ortaksinif.DeclareSecondStep(rightAngle);
                //secondStep elimizde olduğu için TypeTheLetter methoduyla harf yazdırdık
                SendKeys.Send(ortaksinif.TypeTheLetter(firstStep, secondStep, upOrLow));
                movingPartRight.Text = ortaksinif.TypeTheLetter(firstStep, secondStep, upOrLow);
                general_MouseUp(null, null);
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
            leftGuideY = e.Y - (movingPartLeft.Height / 2);
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
