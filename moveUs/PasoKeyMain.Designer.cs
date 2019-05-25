﻿namespace moveUs
{
    partial class PasoKeyMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasoKeyMain));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markingMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.applicationExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markingMenuToolStripMenuItem,
            this.joystickToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.applicationExitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 154);
            // 
            // markingMenuToolStripMenuItem
            // 
            this.markingMenuToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.markingMenuToolStripMenuItem.Name = "markingMenuToolStripMenuItem";
            this.markingMenuToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.markingMenuToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.markingMenuToolStripMenuItem.Text = "marking menu";
            this.markingMenuToolStripMenuItem.Click += new System.EventHandler(this.markingMenuToolStripMenuItem_Click);
            // 
            // joystickToolStripMenuItem
            // 
            this.joystickToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.joystickToolStripMenuItem.Name = "joystickToolStripMenuItem";
            this.joystickToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.joystickToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.joystickToolStripMenuItem.Text = "joystick";
            this.joystickToolStripMenuItem.Click += new System.EventHandler(this.joystickToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.closeToolStripMenuItem.Text = "start";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PasoKey";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // applicationExitToolStripMenuItem
            // 
            this.applicationExitToolStripMenuItem.Name = "applicationExitToolStripMenuItem";
            this.applicationExitToolStripMenuItem.Size = new System.Drawing.Size(210, 32);
            this.applicationExitToolStripMenuItem.Text = "application exit";
            this.applicationExitToolStripMenuItem.Click += new System.EventHandler(this.applicationExitToolStripMenuItem_Click);
            // 
            // PasoKeyMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(85, 180);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "PasoKeyMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "main";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.PasoKeyMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem markingMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joystickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem applicationExitToolStripMenuItem;
    }
}