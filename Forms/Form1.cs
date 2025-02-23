﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Teachers__Schedule_Management.Class;
using Teachers__Schedule_Management.Forms;
using Teachers__Schedule_Management.Home;
using Teachers__Schedule_Management.NewFolder1;
using Teachers__Schedule_Management.User_Control;

namespace Teachers__Schedule_Management
{
    public partial class Form1 : Form
    {
        private UpdateUI _updateManager = new UpdateUI();
        private UpdateHelper updater = new UpdateHelper();


        public Form1()
        {
            InitializeComponent();

            InitializeAsync();

            _updateManager.UpdateProgramVersionLabel(Program_Version_Label);

            //start with home page
            Page1_panel.Controls.Clear();
            Home_Page home_page = new Home_Page();
            Page1_panel.Controls.Add(home_page);
        }

        private async void InitializeAsync()
        {
            await updater.CheckForUpdates();
        }


        private void Schedule_button_Click(object sender, EventArgs e)
        {
            // Load the Schedule to Page1_panel
            Page1_panel.Controls.Clear();
            Schedule_Show_v0 schedule = new Schedule_Show_v0();
            Page1_panel.Controls.Add(schedule);
        }

        private void Add_schedule_button_Click(object sender, EventArgs e)
        {
            Page1_panel.Controls.Clear();
            Schedule_Add_v0 schedule = new Schedule_Add_v0();
            Page1_panel.Controls.Add(schedule);
        }

        private void Edit_Delete_button_Click(object sender, EventArgs e)
        {
            Page1_panel.Controls.Clear();
            Schedule_Edit_Delete_v0 schedule = new Schedule_Edit_Delete_v0();
            Page1_panel.Controls.Add(schedule);
        }

        private void Record_button_Click(object sender, EventArgs e)
        {
            Record_Form recordForm = new Record_Form();
            recordForm.ShowDialog();
        }

        private void Delete_record_button_Click(object sender, EventArgs e)
        {
            // Confirm deletion
            if (MessageBox.Show("Are you sure you want to delete the reserve record?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Create an instance of TeacherDataLoader
                    TeacherDataLoader dataLoader = new TeacherDataLoader();

                    // Call DeleteReserveRecord method
                    if (dataLoader.DeleteReserveRecord() == true)
                    {
                        MessageBox.Show("Reserve record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        // Load the Schedule to Page1_panel
                        Page1_panel.Controls.Clear();
                        Schedule_Show_v0 schedule = new Schedule_Show_v0();
                        Page1_panel.Controls.Add(schedule);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Deletion canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Home_Button_Click(object sender, EventArgs e)
        {
            Page1_panel.Controls.Clear();
            Home_Page home_page = new Home_Page();
            Page1_panel.Controls.Add(home_page);
        }

        private void Delete_monthly_log_Button_Click(object sender, EventArgs e)
        {
            // Confirm deletion
            if (MessageBox.Show("Are you sure you want to delete the monthly log?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Create an instance of TeacherDataLoader
                    TeacherDataLoader dataLoader = new TeacherDataLoader();
                    // Call DeleteMonthlyLog method
                    if (dataLoader.DeleteMonthlyLog() == true)
                    {
                        MessageBox.Show("Monthly log deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Deletion canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void foreverMaximize1_Click(object sender, EventArgs e)
        {
            //resiz control
            if (this.WindowState == FormWindowState.Normal)
            {
                Page1_panel.Font = new Font("Tahoma", 18);
            }
            else
            {
                Page1_panel.Font = new Font("Tahoma", 10);
            }
        }
    }
}
