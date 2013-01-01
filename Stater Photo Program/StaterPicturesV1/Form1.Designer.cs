namespace StaterOrganizer
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadStatersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSNPFromcsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSNPForStaterPhotosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSNPForActivitiesRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Picture = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(88, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadStatersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(266, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadStatersToolStripMenuItem
            // 
            this.loadStatersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createSNPFromcsvToolStripMenuItem,
            this.loadSNPForStaterPhotosToolStripMenuItem,
            this.loadSNPForActivitiesRegistrationToolStripMenuItem,
            this.clearCurrentListToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.loadStatersToolStripMenuItem.Name = "loadStatersToolStripMenuItem";
            this.loadStatersToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.loadStatersToolStripMenuItem.Text = "File";
            // 
            // createSNPFromcsvToolStripMenuItem
            // 
            this.createSNPFromcsvToolStripMenuItem.Name = "createSNPFromcsvToolStripMenuItem";
            this.createSNPFromcsvToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.createSNPFromcsvToolStripMenuItem.Text = "City Photos";
            this.createSNPFromcsvToolStripMenuItem.Click += new System.EventHandler(this.createSNPFromcsvToolStripMenuItem_Click);
            // 
            // loadSNPForStaterPhotosToolStripMenuItem
            // 
            this.loadSNPForStaterPhotosToolStripMenuItem.Name = "loadSNPForStaterPhotosToolStripMenuItem";
            this.loadSNPForStaterPhotosToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.loadSNPForStaterPhotosToolStripMenuItem.Text = "Individual Stater Photos";
            this.loadSNPForStaterPhotosToolStripMenuItem.Click += new System.EventHandler(this.loadSNPForStaterPhotosToolStripMenuItem_Click);
            // 
            // loadSNPForActivitiesRegistrationToolStripMenuItem
            // 
            this.loadSNPForActivitiesRegistrationToolStripMenuItem.Name = "loadSNPForActivitiesRegistrationToolStripMenuItem";
            this.loadSNPForActivitiesRegistrationToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.loadSNPForActivitiesRegistrationToolStripMenuItem.Text = "Stater Registration";
            this.loadSNPForActivitiesRegistrationToolStripMenuItem.Click += new System.EventHandler(this.loadSNPForActivitiesRegistrationToolStripMenuItem_Click);
            // 
            // clearCurrentListToolStripMenuItem
            // 
            this.clearCurrentListToolStripMenuItem.Name = "clearCurrentListToolStripMenuItem";
            this.clearCurrentListToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.clearCurrentListToolStripMenuItem.Text = "Clear Current List";
            this.clearCurrentListToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentListToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Picture
            // 
            this.Picture.Enabled = false;
            this.Picture.Location = new System.Drawing.Point(12, 87);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(102, 175);
            this.Picture.TabIndex = 4;
            this.Picture.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(105, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Load SNP";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(131, 87);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(122, 173);
            this.listBox1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 288);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Stater Organizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadStatersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RichTextBox Picture;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentListToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem createSNPFromcsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSNPForStaterPhotosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSNPForActivitiesRegistrationToolStripMenuItem;
    }

}

