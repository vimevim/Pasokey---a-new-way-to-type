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
    public partial class MarkingMenu : Form
    {
        private void CursorInTheCentre()
        {
            Point pt = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2);// değerleri ikiye bölüp
            Cursor.Position = (pt);//merkeze atadık
        }

        int defaultOriginX, defaultOriginY;
        int formOriginX, formOriginY;
        int formCentreX, formCentreY;
        double hypotenuse, angle;
        bool i = false;
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
        private void WriteFirstStepToLbl(int birinciDeger, int ikinciDeger)
        {
            if (i == false)
            {
                lblUp.Text = keyPad[birinciDeger, 0];
                lblUpRight.Text = keyPad[birinciDeger, 1];
                lblMidRight.Text = keyPad[birinciDeger, 2];
                lblDownRight.Text = keyPad[birinciDeger, 3];
                lblDownMid.Text = keyPad[birinciDeger, 4];
                lblDownLeft.Text = keyPad[birinciDeger, 5];
                lblMidLeft.Text = keyPad[birinciDeger, 6];
                lblUpLeft.Text = keyPad[birinciDeger, 7];
                TypeTheLetter(i, birinciDeger, ikinciDeger);
            }
            else
            {
                ResetText();
                TypeTheLetter(i, birinciDeger, ikinciDeger);
            }
        }

        private void ResetText()
        {
            lblUp.Text = "X";
            lblUpRight.Text = "X";
            lblMidRight.Text = "X";
            lblDownRight.Text = "X";
            lblDownMid.Text = "X";
            lblDownLeft.Text = "X";
            lblMidLeft.Text = "X";
            lblUpLeft.Text = "X";
            lblCenter.Text = "<";
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



        public MarkingMenu()
        {
            InitializeComponent();
            formCentreX = this.Width / 2;
            formCentreY = this.Height / 2;
            CursorInTheCentre();
        }

        public void MarkingMenuMouseMove(object sender, MouseEventArgs e)
        {
            //Varsayılan orijin ataması yapılıyor
            defaultOriginX = e.X;
            defaultOriginY = e.Y;
            //mouse merkezi orijin olarak atanıyor, bu komutun üstte olması lazım.
            formOriginX = defaultOriginX - formCentreX;//
            formOriginY = defaultOriginY - formCentreY;

            //orijine göre hypotenuse hesaplaması
            hypotenuse = Math.Sqrt(formOriginX * formOriginX + formOriginY * formOriginY);

            //orijin ve hipotenüs ile açı hesaplaması - 360 ve 0 derece çakışınca bize 360 veriyor
            angle = Math.Acos(formOriginY / hypotenuse);
            if (formOriginX < 0)
            {
                angle = angle * 180 / Math.PI;
            }
            else
            {
                angle -= angle * 180 / Math.PI;
                angle += 360;
            }
            ////*Farenin konumları yazdırılıyor*////

            //farenin normal konumu
            labelEX.Text = Convert.ToString(defaultOriginX);
            labelEY.Text = Convert.ToString(defaultOriginY);

            //farenin orijine göre konumu
            labelEXW.Text = Convert.ToString(formOriginX);
            labelEYH.Text = Convert.ToString(formOriginY);

            //farenin açısı
            lblAngle.Text = Convert.ToString(angle);

            //hypotenuse kontrolü ve yazdırılması
            labelHipotenus.Text = Convert.ToString(hypotenuse);
            
            //çizgi başlangıcı
            Graphics g = this.CreateGraphics();
            Pen d = new Pen(Color.FromArgb(48, 47, 55), 5);//çizici kalem
            Pen s = new Pen(Color.FromArgb(245, 245, 245), 5);//iz silici kalem, sıkıntısı var
            if (e.Button == MouseButtons.Right)
            {
                int angularEdge = ((Math.Abs(formOriginY) + Math.Abs(formOriginX)) / 2);
                if (angle > 337.5 || angle < 22.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                    g.DrawLine(s, formCentreX, formCentreY, formCentreX, defaultOriginY);//çizginin izi siliniyor
                }
                else if (angle > 22.5 && angle < 67.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (angularEdge + formCentreY));
                    g.DrawLine(s, formCentreX, formCentreY, (formCentreX - angularEdge), (angularEdge + formCentreY));
                }
                else if (angle > 67.5 && angle < 112.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                    g.DrawLine(s, formCentreX, formCentreY, defaultOriginX, formCentreY);//çizginin izi siliniyor
                }
                else if (angle > 112.5 && angle < 157.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, (formCentreX - angularEdge), (formCentreY - angularEdge));
                    g.DrawLine(s, formCentreX, formCentreY, (formCentreX - angularEdge), (formCentreY - angularEdge));
                }
                else if (angle > 157.5 && angle < 202.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, formCentreX, defaultOriginY);//çiziliyor
                    g.DrawLine(s, formCentreX, formCentreY, formCentreX, defaultOriginY);//çizginin izi siliniyor
                }
                else if (angle > 202.5 && angle < 247.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (formCentreY - angularEdge));
                    g.DrawLine(s, formCentreX, formCentreY, (angularEdge + formCentreX), (formCentreY - angularEdge));
                }
                else if (angle > 247.5 && angle < 292.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, formCentreY);//çiziliyor
                    g.DrawLine(s, formCentreX, formCentreY, defaultOriginX, formCentreY);//çizginin izi siliniyor
                }
                else if (angle > 292.5 && angle < 337.5)
                {
                    g.DrawLine(d, formCentreX, formCentreY, (angularEdge + formCentreX), (angularEdge + formCentreY));
                    g.DrawLine(s, formCentreX, formCentreY, (angularEdge + formCentreX), (angularEdge + formCentreY));
                }
                else
                {
                    g.DrawLine(d, formCentreX, formCentreY, defaultOriginX, defaultOriginY);//çiziliyor
                    g.DrawLine(s, formCentreX, formCentreY, defaultOriginX, defaultOriginY);//çizginin izi siliniyor
                }
            }
            else
            {
                FormFollowsCursor();
            }
        }

        private void MarkingMenuMouseUp(object sender, MouseEventArgs e)
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
            //Form1_Click(null, null);//formun dışına çıkınca çalışmasını sağlıyor ama hata var
            FormFollowsCursor();
        }

        private void MarkingMenu_Load(object sender, EventArgs e)
        {
            FormFollowsCursor();
            //this.TransparencyKey = BackColor;
        }

        private void FormFollowsCursor()
        {//formun konumu 2 ye bölünüp farenin konumundan çıkartıldı,doğal olarak form farenin merkezine geçmiş oldu
            this.Location = new Point(MousePosition.X - formCentreX, MousePosition.Y - formCentreY);
        }
        private void MarkingMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }
    }
}

