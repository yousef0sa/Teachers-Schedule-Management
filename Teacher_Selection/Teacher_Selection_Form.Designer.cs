namespace Teachers__Schedule_Management.User_Control
{
    partial class Teacher_Selection_Form
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
            this.Main_panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Main_panel1
            // 
            this.Main_panel1.AutoScroll = true;
            this.Main_panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Main_panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_panel1.Location = new System.Drawing.Point(0, 0);
            this.Main_panel1.MinimumSize = new System.Drawing.Size(261, 61);
            this.Main_panel1.Name = "Main_panel1";
            this.Main_panel1.Size = new System.Drawing.Size(952, 608);
            this.Main_panel1.TabIndex = 0;
            // 
            // Teacher_Selection_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 608);
            this.Controls.Add(this.Main_panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(126, 50);
            this.Name = "Teacher_Selection_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "themeForm1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Teacher_Selection_Form_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Main_panel1;
    }
}