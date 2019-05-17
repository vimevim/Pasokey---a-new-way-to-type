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
                    btnLbl0.Text = keyPadLower[firstStep, 0];
                    btnLbl1.Text = keyPadLower[firstStep, 1];
                    btnLbl2.Text = keyPadLower[firstStep, 2];
                    btnLbl3.Text = keyPadLower[firstStep, 3];
                    btnLbl4.Text = keyPadLower[firstStep, 4];
                    btnLbl5.Text = keyPadLower[firstStep, 5];
                    btnLbl6.Text = keyPadLower[firstStep, 6];
                    btnLbl7.Text = keyPadLower[firstStep, 7];
                }
                else
                {
                    btnLbl0.Text = keyPadUpper[firstStep, 0];
                    btnLbl1.Text = keyPadUpper[firstStep, 1];
                    btnLbl2.Text = keyPadUpper[firstStep, 2];
                    btnLbl3.Text = keyPadUpper[firstStep, 3];
                    btnLbl4.Text = keyPadUpper[firstStep, 4];
                    btnLbl5.Text = keyPadUpper[firstStep, 5];
                    btnLbl6.Text = keyPadUpper[firstStep, 6];
                    btnLbl7.Text = keyPadUpper[firstStep, 7];
                }
                panel2.BringToFront();//panel2 yi ön plana çıkartıyorum
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
                    rightGuide.Text = keyPadLower[firstStep, secondStep];//seçilen karakter hafızada buton değeri olarak tutuluyor
                }
                else
                {
                    SendKeys.Send(keyPadUpper[firstStep, secondStep]);
                    rightGuide.Text = keyPadUpper[firstStep, secondStep];
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
            panel2.Location = new Point(this.Width - panel2.Width, 0);//panel2 konumunu başlangıçta sağa yaslayan komut.
            this.TransparencyKey = BackColor;
            btnLeftDivider.Visible = false;
        }
        //butonları hareket ettirme komutları burada başlıyor
        private void ortak_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
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

            leftGuideX = e.X + leftGuide.Left - mouseDownLocation.X;
            leftGuideY = e.Y + leftGuide.Top - mouseDownLocation.Y;
            cursorOriginX = leftGuideX + 25 - (panel1.Width / 2);
            cursorOriginY = leftGuideY + 25 - (panel1.Height / 2);

            //if the cursor moves on top of the button
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if the button click continous
            {
                if (leftHypotenuse < 125)//daire içerisinden çıkartmıyor
                {
                    if (cursorOriginX < 125 && cursorOriginX > -125)//x ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        leftGuide.Left = leftGuideX;
                    }
                    if (cursorOriginY < 125 && cursorOriginY > -125)//y ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        leftGuide.Top = leftGuideY;
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

            rightGuideX = e.X + rightGuide.Left - mouseDownLocation.X;
            rightGuideY = e.Y + rightGuide.Top - mouseDownLocation.Y;
            cursorOriginX = rightGuideX + 25 - (panel2.Width / 2);//left değeri bize butonun merkezinden değil sol başlangıcından değer veriyor 
            cursorOriginY = rightGuideY + 25 - (panel2.Height / 2);// top ta öyle, bende butonun yarısı kadar değer verip butonun merkezini simüle ediyorum.

            //if the cursor moves on top of the button
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if the button click continous
            {
                if (rightHypotenuse < 100)//daire içerisinden çıkartmıyor
                {
                    if (cursorOriginX < 100 && cursorOriginX > -100)//x ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        rightGuide.Left = rightGuideX;
                    }
                    if (cursorOriginY < 100 && cursorOriginY > -100)//y ekseninde 100 ile -100 arasından çıkartmıyor
                    {
                        rightGuide.Top = rightGuideY;
                    }
                }
            }
            else//butona basmayı bırakınca buton orjinal konumuna dönüyorr.
            {
                general_MouseUp(null, null);
            }
        }


        private void btnLeftUniter_Click(object sender, EventArgs e)
        {
            this.Width = panel1.Width;//formun boyutu bir panel boyutuyla eşitlenir
            this.Location = new Point(screenWidth - this.Width, screenHeight - this.Height);//formun konumu sağ alta sabitlenir
            panel2.Location = new Point(0, 0);//panel2 konumu panel1 ile üst üste getirilir
            btnLeftDivider.Visible = true;
            btnLeftUniter.Visible = false;
            panel1.BringToFront();
        }

        private void btnLeftDivider_Click(object sender, EventArgs e)
        {
            //formu yeniden yükler, başlangıç konumuna döner
            this.Controls.Clear();
            this.InitializeComponent();
            DualPanel_Load(null, null);
        }

        //buton hareketleri için olan komutlar burada bitiyor

        private void general_MouseUp(object sender, MouseEventArgs e)
        {
            leftGuide.Left = 150;
            leftGuide.Top = 150;
            rightGuide.Left = 150;
            rightGuide.Top = 150;
        }
        private void leftGuide_Click(object sender, EventArgs e)
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

        private void rightGuide_Click(object sender, EventArgs e)
        {
            if (rightHypotenuse <= 50)
            {
                panel1.BringToFront();
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
    }
}
