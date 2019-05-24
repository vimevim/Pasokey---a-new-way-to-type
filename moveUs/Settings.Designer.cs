namespace moveUs
{
    partial class Settings
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
            this.chckAcilistaCalistir = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chckAcilistaCalistir
            // 
            this.chckAcilistaCalistir.AutoSize = true;
            this.chckAcilistaCalistir.Location = new System.Drawing.Point(178, 12);
            this.chckAcilistaCalistir.Name = "chckAcilistaCalistir";
            this.chckAcilistaCalistir.Size = new System.Drawing.Size(98, 21);
            this.chckAcilistaCalistir.TabIndex = 0;
            this.chckAcilistaCalistir.Text = "checkBox1";
            this.chckAcilistaCalistir.UseVisualStyleBackColor = true;
            this.chckAcilistaCalistir.CheckedChanged += new System.EventHandler(this.chckAcilistaCalistir_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chckAcilistaCalistir);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chckAcilistaCalistir;
    }
}