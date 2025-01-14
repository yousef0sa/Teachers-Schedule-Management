namespace Teachers__Schedule_Management
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
            this.Main_panel = new System.Windows.Forms.Panel();
            this.Page1_panel = new System.Windows.Forms.Panel();
            this.Sid_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Delete_record_button = new System.Windows.Forms.Button();
            this.Edit_Delete_button = new System.Windows.Forms.Button();
            this.Add_schedule_button = new System.Windows.Forms.Button();
            this.button_flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Schedule_button = new System.Windows.Forms.Button();
            this.Record_button = new System.Windows.Forms.Button();
            this.Main_panel.SuspendLayout();
            this.Sid_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.button_flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_panel
            // 
            this.Main_panel.Controls.Add(this.Page1_panel);
            this.Main_panel.Controls.Add(this.Sid_panel);
            this.Main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_panel.Location = new System.Drawing.Point(0, 0);
            this.Main_panel.Name = "Main_panel";
            this.Main_panel.Size = new System.Drawing.Size(839, 509);
            this.Main_panel.TabIndex = 0;
            // 
            // Page1_panel
            // 
            this.Page1_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Page1_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Page1_panel.Location = new System.Drawing.Point(3, 3);
            this.Page1_panel.Name = "Page1_panel";
            this.Page1_panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Page1_panel.Size = new System.Drawing.Size(699, 503);
            this.Page1_panel.TabIndex = 1;
            // 
            // Sid_panel
            // 
            this.Sid_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Sid_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Sid_panel.Controls.Add(this.panel1);
            this.Sid_panel.Controls.Add(this.button_flowLayoutPanel);
            this.Sid_panel.Location = new System.Drawing.Point(708, 3);
            this.Sid_panel.Name = "Sid_panel";
            this.Sid_panel.Size = new System.Drawing.Size(128, 503);
            this.Sid_panel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Delete_record_button);
            this.panel1.Controls.Add(this.Edit_Delete_button);
            this.panel1.Controls.Add(this.Add_schedule_button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 126);
            this.panel1.TabIndex = 1;
            // 
            // Delete_record_button
            // 
            this.Delete_record_button.BackColor = System.Drawing.Color.BurlyWood;
            this.Delete_record_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Delete_record_button.Location = new System.Drawing.Point(2, 3);
            this.Delete_record_button.Name = "Delete_record_button";
            this.Delete_record_button.Size = new System.Drawing.Size(122, 33);
            this.Delete_record_button.TabIndex = 3;
            this.Delete_record_button.Text = "حذف سجل الاحتياط";
            this.Delete_record_button.UseVisualStyleBackColor = false;
            this.Delete_record_button.Click += new System.EventHandler(this.Delete_record_button_Click);
            // 
            // Edit_Delete_button
            // 
            this.Edit_Delete_button.Location = new System.Drawing.Point(2, 42);
            this.Edit_Delete_button.Name = "Edit_Delete_button";
            this.Edit_Delete_button.Size = new System.Drawing.Size(122, 33);
            this.Edit_Delete_button.TabIndex = 2;
            this.Edit_Delete_button.Text = "تعديل او حذف الجدول";
            this.Edit_Delete_button.UseVisualStyleBackColor = true;
            this.Edit_Delete_button.Click += new System.EventHandler(this.Edit_Delete_button_Click);
            // 
            // Add_schedule_button
            // 
            this.Add_schedule_button.Location = new System.Drawing.Point(2, 81);
            this.Add_schedule_button.Name = "Add_schedule_button";
            this.Add_schedule_button.Size = new System.Drawing.Size(122, 33);
            this.Add_schedule_button.TabIndex = 1;
            this.Add_schedule_button.Text = "اضافة جدول";
            this.Add_schedule_button.UseVisualStyleBackColor = true;
            this.Add_schedule_button.Click += new System.EventHandler(this.Add_schedule_button_Click);
            // 
            // button_flowLayoutPanel
            // 
            this.button_flowLayoutPanel.Controls.Add(this.Schedule_button);
            this.button_flowLayoutPanel.Controls.Add(this.Record_button);
            this.button_flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.button_flowLayoutPanel.Name = "button_flowLayoutPanel";
            this.button_flowLayoutPanel.Size = new System.Drawing.Size(126, 339);
            this.button_flowLayoutPanel.TabIndex = 0;
            // 
            // Schedule_button
            // 
            this.Schedule_button.Location = new System.Drawing.Point(3, 3);
            this.Schedule_button.Name = "Schedule_button";
            this.Schedule_button.Size = new System.Drawing.Size(122, 33);
            this.Schedule_button.TabIndex = 0;
            this.Schedule_button.Text = "الجدول";
            this.Schedule_button.UseVisualStyleBackColor = true;
            this.Schedule_button.Click += new System.EventHandler(this.Schedule_button_Click);
            // 
            // Record_button
            // 
            this.Record_button.Location = new System.Drawing.Point(3, 42);
            this.Record_button.Name = "Record_button";
            this.Record_button.Size = new System.Drawing.Size(122, 33);
            this.Record_button.TabIndex = 1;
            this.Record_button.Text = "تقرير";
            this.Record_button.UseVisualStyleBackColor = true;
            this.Record_button.Click += new System.EventHandler(this.Record_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 509);
            this.Controls.Add(this.Main_panel);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Main_panel.ResumeLayout(false);
            this.Sid_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.button_flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Main_panel;
        private System.Windows.Forms.Panel Page1_panel;
        private System.Windows.Forms.Panel Sid_panel;
        private System.Windows.Forms.FlowLayoutPanel button_flowLayoutPanel;
        private System.Windows.Forms.Button Schedule_button;
        private System.Windows.Forms.Button Record_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Edit_Delete_button;
        private System.Windows.Forms.Button Add_schedule_button;
        private System.Windows.Forms.Button Delete_record_button;
    }
}

