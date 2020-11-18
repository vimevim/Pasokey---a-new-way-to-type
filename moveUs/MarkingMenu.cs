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
    public partial class MarkingMenu : Form
    {
        double leftHypotenuse, leftAngle, rightHypotenuse, rightAngle;
        int defaultOriginX, defaultOriginY;
        int formOriginX, formOriginY;
        int formCentreX, formCentreY;
        public MarkingMenu()
        {
            InitializeComponent();
            this.Width = leftGuide.Width;
            formCentreX = this.Width / 2;
            formCentreY = this.Height / 2;

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
                pickFormOrCursor();
            }
        }
        //yansıtma yapılmış paneldeki aksiyona göre klavye girdisi veriyorum

        private void thisHide(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        public void MarkingMenu_Load(object sender, EventArgs e)
        {
            pickFormOrCursor();
            if (ayarlar.Default.theMod == "FloatingMarkingMenu")
            {
                leftGuide.Location = new Point(0, 0);
                rightGuide.Location = new Point(0, 0);//panel2 konumu panel1 ile üst üste getirilir
                this.TransparencyKey = BackColor;
                leftGuide.BringToFront();
            }
            else if (ayarlar.Default.theMod == "FixedMarkingMenu")
            {
                leftGuide.Location = new Point(0, 0);
                rightGuide.Location = new Point(0, 0);//panel2 konumu panel1 ile üst üste getirilir
                this.TransparencyKey = BackColor;
                leftGuide.BringToFront();
            }
        }
        private void pickFormOrCursor()
        {
            if (ayarlar.Default.theMod == "FloatingMarkingMenu")
            {
                //FormFollowsCursor();
                //formun konumu 2 ye bölünüp farenin konumundan çıkartıldı,doğal olarak form farenin merkezine geçmiş oldu
                this.Location = new Point(Cursor.Position.X - formCentreX, Cursor.Position.Y - formCentreY);
            }
            else if (ayarlar.Default.theMod == "FixedMarkingMenu")
            {
                //CursorInTheCentre();
                Point pt = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2);// değerleri ikiye bölüp
                Cursor.Position = (pt);//merkeze atadık
                this.Location = new Point(pt.X - formCentreX, pt.Y - formCentreY);
            }
        }
        private void leftGuide_MouseMove(object sender, MouseEventArgs e)
        {
            //Varsayılan orijin ataması yapılıyor
            defaultOriginX = e.X;
            defaultOriginY = e.Y;
            //mouse merkezi orijin olarak atanıyor, bu komutun üstte olması lazım.
            formOriginX = defaultOriginX - formCentreX;//
            formOriginY = defaultOriginY - formCentreY;

            //orijine göre hypotenuse hesaplaması
            leftHypotenuse = Math.Sqrt(formOriginX * formOriginX + formOriginY * formOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            leftAngle = Math.Acos(formOriginY / leftHypotenuse);
            if (formOriginX < 0)
            {
                leftAngle = leftAngle * 180 / Math.PI;
            }
            else
            {
                leftAngle -= leftAngle * 180 / Math.PI;
                leftAngle += 360;
            }

            //çizgi başlangıcı
            Graphics g = leftGuide.CreateGraphics();
            Pen d = new Pen(Color.FromArgb(48, 47, 55), 5);//çizici kalem
            if (e.Button == MouseButtons.Right)
            {
                int angularEdge = ((Math.Abs(formOriginY) + Math.Abs(formOriginX)) / 2);
                if (leftAngle > 337.5 || leftAngle < 22.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                }
                else if (leftAngle > 22.5 && leftAngle < 67.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (angularEdge + formCentreY));
                    }
                }
                else if (leftAngle > 67.5 && leftAngle < 112.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                }
                else if (leftAngle > 112.5 && leftAngle < 157.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (formCentreY - angularEdge));
                    }
                }
                else if (leftAngle > 157.5 && leftAngle < 202.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                }
                else if (leftAngle > 202.5 && leftAngle < 247.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (formCentreY - angularEdge));
                    }
                }
                else if (leftAngle > 247.5 && leftAngle < 292.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                }
                else if (leftAngle > 292.5 && leftAngle < 337.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (angularEdge + formCentreY));
                    }
                }
                else
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, defaultOriginY);//çiziliyor
                }
                leftGuide.Invalidate();
            }
            else
            {
                pickFormOrCursor();
            }
        }

        private void leftGuide_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
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

                    }
                }
                pickFormOrCursor();
            }
        }

        private void rightGuide_MouseMove(object sender, MouseEventArgs e)
        {
            //Varsayılan orijin ataması yapılıyor
            defaultOriginX = e.X;
            defaultOriginY = e.Y;
            //mouse merkezi orijin olarak atanıyor, bu komutun üstte olması lazım.
            formOriginX = defaultOriginX - formCentreX;//
            formOriginY = defaultOriginY - formCentreY;

            //orijine göre hypotenuse hesaplaması
            rightHypotenuse = Math.Sqrt(formOriginX * formOriginX + formOriginY * formOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            rightAngle = Math.Acos(formOriginY / rightHypotenuse);
            if (formOriginX < 0)
            {
                rightAngle = rightAngle * 180 / Math.PI;
            }
            else
            {
                rightAngle -= rightAngle * 180 / Math.PI;
                rightAngle += 360;
            }

            //çizgi başlangıcı
            Graphics g = rightGuide.CreateGraphics();
            Pen d = new Pen(Color.FromArgb(48, 47, 55), 5);//çizici kalem
            if (e.Button == MouseButtons.Right)
            {
                int angularEdge = ((Math.Abs(formOriginY) + Math.Abs(formOriginX)) / 2);
                if (rightAngle > 337.5 || rightAngle < 22.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                }
                else if (rightAngle > 22.5 && rightAngle < 67.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (angularEdge + formCentreY));
                    }
                }
                else if (rightAngle > 67.5 && rightAngle < 112.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                }
                else if (rightAngle > 112.5 && rightAngle < 157.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (formCentreY - angularEdge));
                    }
                }
                else if (rightAngle > 157.5 && rightAngle < 202.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                }
                else if (rightAngle > 202.5 && rightAngle < 247.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (formCentreY - angularEdge));
                    }
                }
                else if (rightAngle > 247.5 && rightAngle < 292.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                }
                else if (rightAngle > 292.5 && rightAngle < 337.5)
                {
                    if (leftHypotenuse < formCentreX)
                    {
                        g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (angularEdge + formCentreY));
                    }
                }
                else
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, defaultOriginY);//çiziliyor
                }
                rightGuide.Invalidate();
            }
            else
            {
                pickFormOrCursor();
            }
        }

        private void rightGuide_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (rightHypotenuse <= 50)
                {
                    leftGuide.BringToFront();
                }
                else
                {
                    secondStep = ortaksinif.DeclareSecondStep(rightAngle);
                    SendKeys.Send(ortaksinif.TypeTheLetter(firstStep, secondStep, upOrLow));
                    rightLblCentre.Text = ortaksinif.TypeTheLetter(firstStep, secondStep, upOrLow);
                    pickFormOrCursor();
                }
            }
        }

    }
}
