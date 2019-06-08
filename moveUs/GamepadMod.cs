using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenJigWare;

namespace PasoKey
{
    public partial class GamepadMod : Form
    {
        public GamepadMod()
        {
            InitializeComponent();
            this.TransparencyKey = BackColor;
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


        private void GamepadMod_Load(object sender, EventArgs e)
        {
            joystickTimer.Enabled = true;
        }
        private Ojw.CJoystick m_CJoy = new Ojw.CJoystick(Ojw.CJoystick._ID_0);
        private Ojw.CTimer m_CTmr_Joystick = new Ojw.CTimer();

        //joystick bağlantısını kontrol ediyoruz
        private void FJoystick_Check_Alive()
        {
            Color m_colorLive = Color.Green;
            Color m_colorDead = Color.Gray;
            if (m_CJoy.IsValid == false)
            {
                if (lbJoystick.ForeColor != m_colorDead)
                {
                    lbJoystick.Text = "Joystick (No Connected)";
                    lbJoystick.ForeColor = m_colorDead;
                }
                if (m_CTmr_Joystick.Get() > 3000)
                {
                    Ojw.CMessage.Write("Joystick Check again");
                    m_CJoy = new Ojw.CJoystick(Ojw.CJoystick._ID_0);

                    if (m_CJoy.IsValid == false)
                    {
                        Ojw.CMessage.Write("But we can't find a joystick device in here. Check your joystick device");
                        m_CTmr_Joystick.Set();
                    }
                    else Ojw.CMessage.Write("Joystick is Connected");
                }
            }
            else
            {

                if (lbJoystick.ForeColor != m_colorLive)
                {
                    lbJoystick.Text = "Joystick (Connected)";
                    lbJoystick.ForeColor = m_colorLive;
                }
            }
        }

        string labelDegeri = "";
        string[,] keyPadLower = new string[5, 8] {
            {"a","b","c","d","e","f","g","h"},
            {"i","j","k","l","m","n","o","p"},
            {"q","r","s","t","u","v","w","x"},
            {"y","z","1","2","3","4","5","6"},
            {"7","8","9","0",".",",","!","?"}
        };
        private void WriteFirstStepToLbl()
        {
            if (firstStep != 5)//buton hareket etse bile bir değer atandı mı emin oluyorum
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
        }

        int firstStep = 5;
        int rightValue = 9;
        bool show = true;
        bool mouseMod = false;
        bool mouseLeftClicked = false;
        bool mouseRightClicked = false;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(uint flag, uint _x, uint _y, uint btn, uint exInfo);
        private const int MOUSEEVENT_LEFTDOWN = 0x02;//Left click pressed
        private const int MOUSEEVENT_LEFTUP = 0x04;//Left click released
        private const int MOUSEEVENT_RIGHTDOWN = 0x08; //Right click pressed
        private const int MOUSEEVENT_RIGHTUP = 0x10; //Right click released

        private void FJoystick_Check_Data()
        {
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.POVLeft) == true)
            {
                labelDegeri = "← ";
            }
            else if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.POVRight) == true)
            {
                labelDegeri = "→ ";
            }
            else
            {
                labelDegeri = "";
            }

            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.POVUp) == true)
            {
                labelDegeri += "↑";
            }
            else if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.POVDown) == true)
            {
                labelDegeri += "↓";
            }

            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button4) == true)
            {
                labelDegeri = "Y";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button3) == true)
            {
                labelDegeri = "X";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button2) == true)
            {
                labelDegeri = "B";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button1) == true)
            {
                labelDegeri = "A";
            }

            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button5) == true)
            {
                labelDegeri = "L1";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button6) == true)
            {
                labelDegeri = "R1";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button7) == true)
            {
                labelDegeri = "L2";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button8) == true)
            {
                labelDegeri = "R2";
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button9) == true)
            {
                labelDegeri = "select";
                if (mouseMod == false)
                {
                    mouseMod = true;
                }
                else if (mouseMod == true)
                {
                    mouseMod = false;
                }
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button10) == true)
            {
                labelDegeri = "start";
                if (show == true)
                {
                    this.Hide();
                    show = false;
                }
                else if (show == false)
                {
                    this.Show();
                    show = true;
                }
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button11) == true)
            {
                movingPartLeft.BackColor = Color.WhiteSmoke;
            }
            if (m_CJoy.IsUp(Ojw.CJoystick.PadKey.Button11) == true)
            {
                movingPartLeft.BackColor = Color.White;
            }
            if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button12) == true)
            {
                movingPartRight.BackColor = Color.WhiteSmoke;
            }
            if (m_CJoy.IsUp(Ojw.CJoystick.PadKey.Button12) == true)
            {
                movingPartRight.BackColor = Color.White;
            }

            label1.Text = labelDegeri;
            //100 ile çarp virgülü yok et
            int leftThumbX = (int)(m_CJoy.GetPos0 * 100);
            int leftThumbY = (int)(m_CJoy.GetPos1 * 100);
            int rightThumbX = (int)(m_CJoy.GetPos2 * 100);
            int rightThumbY = (int)(m_CJoy.GetPos3 * 100);

            movingPartLeft.Location = new Point((int)(300* m_CJoy.GetPos0), (int)(300* m_CJoy.GetPos1));
            movingPartRight.Location = new Point((int)(300 * m_CJoy.GetPos2), (int)(300* m_CJoy.GetPos3));
            if (mouseMod == true)
            {
                MouseMoveLeft(leftThumbX - 49, leftThumbY - 49);
                MouseMoveRight(rightThumbX - 50, rightThumbY - 50);
                if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button11) == true)
                {
                    mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
                    mouseLeftClicked = false;
                }
                if (mouseLeftClicked == false)
                {
                    if (m_CJoy.IsUp(Ojw.CJoystick.PadKey.Button11) == true)
                    {
                        mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);
                        mouseLeftClicked = true;
                    }
                }
                if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button12) == true)
                {
                    mouse_event(MOUSEEVENT_RIGHTDOWN, 0, 0, 0, 0);
                    mouseRightClicked = false;
                }
                if (mouseRightClicked == false)
                {
                    if (m_CJoy.IsUp(Ojw.CJoystick.PadKey.Button12) == true)
                    {
                        mouse_event(MOUSEEVENT_RIGHTUP, 0, 0, 0, 0);
                        mouseRightClicked = true;
                    }
                }
            }
            if (mouseMod == false)
            {
                //clicking 
                if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button11) == true)
                {
                    firstStep = 4;
                }
                if (m_CJoy.IsDown(Ojw.CJoystick.PadKey.Button12) == true)
                {
                    //radiobutton2
                }

                //left side
                if (leftThumbY <= 25 && leftThumbX > 25 && leftThumbX < 75)
                {
                    firstStep = 0;
                }
                else if (leftThumbY >= 75 && leftThumbX > 25 && leftThumbX < 75)
                {
                    firstStep = 2;
                }
                else if (leftThumbX <= 25 && leftThumbY > 25 && leftThumbY < 75)
                {
                    firstStep = 3;
                }
                else if (leftThumbX >= 75 && leftThumbY > 25 && leftThumbY < 75)
                {
                    firstStep = 1;
                }
                else if (leftThumbY <= 25 && leftThumbX >= 75)
                {
                    //backspace
                    SendKeys.Send("{BS}");
                }
                else if (leftThumbY >= 75 && leftThumbX >= 75)
                {
                    //enter
                    SendKeys.Send("{ENTER}");
                }
                else if (leftThumbX <= 25 && leftThumbY >= 75)
                {
                    //capslock
                }
                else if (leftThumbX <= 25 && leftThumbY <= 25)
                {
                    //space
                    SendKeys.Send(" ");
                }
                WriteFirstStepToLbl();

                //right side
                if (rightThumbY <= 25 && rightThumbX > 25 && rightThumbX < 75)
                {
                    rightValue = 0;
                }
                else if (rightThumbY >= 75 && rightThumbX > 25 && rightThumbX < 75)
                {
                    rightValue = 4;
                }
                else if (rightThumbX <= 25 && rightThumbY > 25 && rightThumbY < 75)
                {
                    rightValue = 6;
                }
                else if (rightThumbX >= 75 && rightThumbY > 25 && rightThumbY < 75)
                {
                    rightValue = 2;
                }
                else if (rightThumbY <= 25 && rightThumbX >= 75)
                {
                    rightValue = 1;
                }
                else if (rightThumbY >= 75 && rightThumbX >= 75)
                {
                    rightValue = 3;
                }
                else if (rightThumbX <= 25 && rightThumbY >= 75)
                {
                    rightValue = 5;
                }
                else if (rightThumbX <= 25 && rightThumbY <= 25)
                {
                    rightValue = 7;

                }
                if (rightValue != 9 && firstStep != 5)
                {
                    if (rightThumbX > 30 && rightThumbX < 70 && rightThumbY > 30 && rightThumbY < 70)
                    {
                        SendKeys.Send(keyPadLower[firstStep, rightValue]);
                        rightValue = 9;
                    }
                }
            }
        }

        public void MouseMoveLeft(int posx, int posy)
        {
            Cursor.Position = new Point(Cursor.Position.X + posx, Cursor.Position.Y + posy);
            //Cursor.Clip = new Rectangle(Screen.PrimaryScreen.WorkingArea.Location, Screen.PrimaryScreen.WorkingArea.Size);
        }
        public void MouseMoveRight(int posx, int posy)
        {
            Cursor.Position = new Point(Cursor.Position.X + posx / 7, Cursor.Position.Y + posy / 7);
            //Cursor.Clip = new Rectangle(Screen.PrimaryScreen.WorkingArea.Location, Screen.PrimaryScreen.WorkingArea.Size);
        }
        private void joystickTimer_Tick(object sender, EventArgs e)
        {
            m_CJoy.Update();
            FJoystick_Check_Alive();
            FJoystick_Check_Data();
        }
    }
}
