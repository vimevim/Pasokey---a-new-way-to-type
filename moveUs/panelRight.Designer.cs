﻿namespace PasoKey
{
    partial class PanelRight
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonElipse1 = new PasoKey.ButtonElipse();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dots2 = new System.Windows.Forms.Label();
            this.dots1 = new System.Windows.Forms.Label();
            this.kepenkKapat = new System.Windows.Forms.Timer(this.components);
            this.kepenkAc = new System.Windows.Forms.Timer(this.components);
            this.sleepModeActivate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonElipse1
            // 
            this.buttonElipse1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonElipse1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonElipse1.Location = new System.Drawing.Point(50, 50);
            this.buttonElipse1.Name = "buttonElipse1";
            this.buttonElipse1.Size = new System.Drawing.Size(50, 50);
            this.buttonElipse1.TabIndex = 8;
            this.buttonElipse1.Text = "btn1";
            this.buttonElipse1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 400);
            this.panel2.TabIndex = 7;
            this.panel2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(30, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 400);
            this.panel1.TabIndex = 6;
            // 
            // dots2
            // 
            this.dots2.AutoSize = true;
            this.dots2.Enabled = false;
            this.dots2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dots2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dots2.Location = new System.Drawing.Point(58, 10);
            this.dots2.Name = "dots2";
            this.dots2.Size = new System.Drawing.Size(62, 29);
            this.dots2.TabIndex = 4;
            this.dots2.Text = ":::::::";
            // 
            // dots1
            // 
            this.dots1.AutoSize = true;
            this.dots1.Enabled = false;
            this.dots1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dots1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.dots1.Location = new System.Drawing.Point(58, 361);
            this.dots1.Name = "dots1";
            this.dots1.Size = new System.Drawing.Size(62, 29);
            this.dots1.TabIndex = 5;
            this.dots1.Text = ":::::::";
            // 
            // kepenkKapat
            // 
            this.kepenkKapat.Interval = 10;
            this.kepenkKapat.Tick += new System.EventHandler(this.kepenkKapat_Tick);
            // 
            // kepenkAc
            // 
            this.kepenkAc.Interval = 10;
            this.kepenkAc.Tick += new System.EventHandler(this.kepenkAc_Tick);
            // 
            // sleepModeActivate
            // 
            this.sleepModeActivate.Interval = 1000;
            this.sleepModeActivate.Tick += new System.EventHandler(this.sleepModeActivate_Tick);
            // 
            // PanelRight
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(150, 400);
            this.Controls.Add(this.buttonElipse1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dots2);
            this.Controls.Add(this.dots1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PanelRight";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.panelRight_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelRight_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelRight_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonElipse buttonElipse1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label dots2;
        private System.Windows.Forms.Label dots1;
        private System.Windows.Forms.Timer kepenkKapat;
        private System.Windows.Forms.Timer kepenkAc;
        private System.Windows.Forms.Timer sleepModeActivate;
    }
}