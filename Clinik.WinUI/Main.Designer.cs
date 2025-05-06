namespace Clinik.WinUI
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sapleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sample3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sample4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.sampleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // sampleToolStripMenuItem
            // 
            this.sampleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sapleToolStripMenuItem,
            this.sample3ToolStripMenuItem,
            this.sample4ToolStripMenuItem});
            this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            this.sampleToolStripMenuItem.Size = new System.Drawing.Size(87, 29);
            this.sampleToolStripMenuItem.Text = "Sample";
            // 
            // sapleToolStripMenuItem
            // 
            this.sapleToolStripMenuItem.Name = "sapleToolStripMenuItem";
            this.sapleToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sapleToolStripMenuItem.Text = "Saple";
            this.sapleToolStripMenuItem.Click += new System.EventHandler(this.sapleToolStripMenuItem_Click);
            // 
            // sample3ToolStripMenuItem
            // 
            this.sample3ToolStripMenuItem.Name = "sample3ToolStripMenuItem";
            this.sample3ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sample3ToolStripMenuItem.Text = "Sample3";
            this.sample3ToolStripMenuItem.Click += new System.EventHandler(this.sample3ToolStripMenuItem_Click);
            // 
            // sample4ToolStripMenuItem
            // 
            this.sample4ToolStripMenuItem.Name = "sample4ToolStripMenuItem";
            this.sample4ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sample4ToolStripMenuItem.Text = "Sample4";
            this.sample4ToolStripMenuItem.Click += new System.EventHandler(this.sample4ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 757);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sapleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sample4ToolStripMenuItem;
    }
}