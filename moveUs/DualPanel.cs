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
    public partial class DualPanel : Form
    {
        public DualPanel()
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

        Point mouseDownLocation;

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
        private void Writer()
        {
            if (firstStep != 5)//buton hareket etse bile bir değer atandı mı emin oluyorum
            {
                if (upOrLow == "low")//büyük küçük harf kümesini seçiyorum
                {
                    btn0.Text = keyPadLower[firstStep, 0];
                    btn1.Text = keyPadLower[firstStep, 1];
                    btn2.Text = keyPadLower[firstStep, 2];
                    btn3.Text = keyPadLower[firstStep, 3];
                    btn4.Text = keyPadLower[firstStep, 4];
                    btn5.Text = keyPadLower[firstStep, 5];
                    btn6.Text = keyPadLower[firstStep, 6];
                    btn7.Text = keyPadLower[firstStep, 7];
                }
                else
                {
                    btn0.Text = keyPadUpper[firstStep, 0];
                    btn1.Text = keyPadUpper[firstStep, 1];
                    btn2.Text = keyPadUpper[firstStep, 2];
                    btn3.Text = keyPadUpper[firstStep, 3];
                    btn4.Text = keyPadUpper[firstStep, 4];
                    btn5.Text = keyPadUpper[firstStep, 5];
                    btn6.Text = keyPadUpper[firstStep, 6];
                    btn7.Text = keyPadUpper[firstStep, 7];
                }
                panel2.BringToFront();//panel2 yi ön plana çıkartıyorum
                general_MouseUp(null, null);//tüm butonların konumunu başlangıca çekiyorum
            }
        }
        //yansıtma yapılmış paneldeki aksiyona göre klavye girdisi veriyorum
        private void Write()
        {
            if (firstStep == 5)//ilk aksiyonumuz gerçekleşmediyse ikinci aksiyona geçmeyi engelliyorum
            {
                MessageBox.Show("Lütfen ilk adımı giriniz.");
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
            panel1.BringToFront();//panel1 ön plana çıkıyor
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
            if (e.Button == MouseButtons.Left)
            {
                leftGuide.Left = e.X + leftGuide.Left - mouseDownLocation.X;
                leftGuide.Top = e.Y + leftGuide.Top - mouseDownLocation.Y;
            }
            else
            {
                general_MouseUp(null, null);
            }
        }
        private void rightGuide_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rightGuide.Left = e.X + rightGuide.Left - mouseDownLocation.X;
                rightGuide.Top = e.Y + rightGuide.Top - mouseDownLocation.Y;
            }
            else
            {
                general_MouseUp(null, null);
            }
        }
        private void btnUpper_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnUpper.Left = e.X + btnUpper.Left - mouseDownLocation.X;
                btnUpper.Top = e.Y + btnUpper.Top - mouseDownLocation.Y;
            }
            else
            {
                general_MouseUp(null, null);
            }
        }

        private void btnSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnSpace.Left = e.X + btnSpace.Left - mouseDownLocation.X;
                btnSpace.Top = e.Y + btnSpace.Top - mouseDownLocation.Y;
            }
            else
            {
                general_MouseUp(null, null);
            }
        }

        private void btnBackSpace_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnBackSpace.Left = e.X + btnBackSpace.Left - mouseDownLocation.X;
                btnBackSpace.Top = e.Y + btnBackSpace.Top - mouseDownLocation.Y;
            }
            else
            {
                general_MouseUp(null, null);
            }
        }
        //buton hareketleri için olan komutlar burada bitiyor
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

        private void btnSpace_Click(object sender, EventArgs e)
        {
            if (btnUpper.Top <= 300)
            {
                SendKeys.Send(" ");
            }
            else if (btnUpper.Top >= 320 && btnUpper.Top <= 360 && btnUpper.Left >= 15 && btnUpper.Left <= 235)
            {
                SendKeys.Send(" ");
            }
        }
        private void general_MouseUp(object sender, MouseEventArgs e)
        {
            leftGuide.Left = 150;
            leftGuide.Top = 150;
            rightGuide.Left = 150;
            rightGuide.Top = 150;
            btnUpper.Left = 115;
            btnUpper.Top = 325;
            btnSpace.Left = 115;
            btnSpace.Top = 325;
            btnBackSpace.Left = 325;
            btnBackSpace.Top = 115;
        }
    }
}
