using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Teachers__Schedule_Management.User_Control
{
    public partial class Schedule_Edit_Delete_v0 : UserControl
    {
        private static readonly string MainScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scheduleData.json");
        public Schedule_Edit_Delete_v0()
        {
            InitializeComponent();
            AddButtonClickEvents();
            this.Dock = DockStyle.Fill;

        }

        private void AddButtonClickEvents()
        {
            Sunday_First.Click += ToggleButton_Click;
            Sunday_Second.Click += ToggleButton_Click;
            Sunday_Third.Click += ToggleButton_Click;
            Sunday_Fourth.Click += ToggleButton_Click;
            Sunday_Fifth.Click += ToggleButton_Click;
            Sunday_Sixth.Click += ToggleButton_Click;
            Sunday_Seventh.Click += ToggleButton_Click;
            Monday_First.Click += ToggleButton_Click;
            Tuesday_First.Click += ToggleButton_Click;
            Wednesday_First.Click += ToggleButton_Click;
            Thursday_First.Click += ToggleButton_Click;
            Monday_Second.Click += ToggleButton_Click;
            Tuesday_Second.Click += ToggleButton_Click;
            Wednesday_Second.Click += ToggleButton_Click;
            Thursday_Second.Click += ToggleButton_Click;
            Monday_Third.Click += ToggleButton_Click;
            Tuesday_Third.Click += ToggleButton_Click;
            Wednesday_Third.Click += ToggleButton_Click;
            Thursday_Third.Click += ToggleButton_Click;
            Monday_Fourth.Click += ToggleButton_Click;
            Tuesday_Fourth.Click += ToggleButton_Click;
            Wednesday_Fourth.Click += ToggleButton_Click;
            Thursday_Fourth.Click += ToggleButton_Click;
            Monday_Fifth.Click += ToggleButton_Click;
            Tuesday_Fifth.Click += ToggleButton_Click;
            Wednesday_Fifth.Click += ToggleButton_Click;
            Thursday_Fifth.Click += ToggleButton_Click;
            Monday_Sixth.Click += ToggleButton_Click;
            Tuesday_Sixth.Click += ToggleButton_Click;
            Wednesday_Sixth.Click += ToggleButton_Click;
            Thursday_Sixth.Click += ToggleButton_Click;
            Monday_Seventh.Click += ToggleButton_Click;
            Tuesday_Seventh.Click += ToggleButton_Click;
            Wednesday_Seventh.Click += ToggleButton_Click;
            Thursday_Seventh.Click += ToggleButton_Click;
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.BackColor == Color.Green)
            {
                button.BackColor = SystemColors.ControlLightLight;
            }
            else
            {
                button.BackColor = Color.Green;
            }
        }

        private void Select_comboBox_DropDown(object sender, EventArgs e)
        {
            // Clear existing items
            Select_comboBox.Items.Clear();

            // Load all the Teachers names in the ComboBox from json file
            if (File.Exists(MainScheduleFile))
            {
                string json = File.ReadAllText(MainScheduleFile);
                var scheduleList = JsonConvert.DeserializeObject<List<dynamic>>(json);
                if(scheduleList == null)
                {
                    return;
                }

                foreach (var schedule in scheduleList)
                {
                    if (schedule != null && schedule.TeacherName != null)
                    {
                        Select_comboBox.Items.Add(schedule.TeacherName.ToString());
                    }
                }

                if (Select_comboBox.Items.Count == 0)
                {
                    MessageBox.Show("No teachers found in the schedule data.");
                }
            }
            else
            {
                MessageBox.Show("The schedule data file does not exist.");
            }
        }

        private void Select_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Select_comboBox.SelectedItem == null)
            {
                return;
            }

            var selectedTeacher = Select_comboBox.SelectedItem.ToString();

            if (File.Exists(MainScheduleFile))
            {
                string json = File.ReadAllText(MainScheduleFile);
                var scheduleList = JsonConvert.DeserializeObject<List<dynamic>>(json);

                var teacherSchedule = scheduleList.FirstOrDefault(schedule => schedule.TeacherName == selectedTeacher);

                if (teacherSchedule != null && teacherSchedule.Schedule != null)
                {
                    LoadSchedule(teacherSchedule.Schedule);
                }
                else
                {
                    MessageBox.Show("Selected teacher's schedule not found.");
                }
            }
            else
            {
                MessageBox.Show("The schedule data file does not exist.");
            }
        }

        private void LoadSchedule(dynamic schedule)
        {
            // Reset all button colors first
            ResetButtonColors(this);

            // Set button colors based on the schedule
            Sunday_First.BackColor = schedule.Sunday_First == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Second.BackColor = schedule.Sunday_Second == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Third.BackColor = schedule.Sunday_Third == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Fourth.BackColor = schedule.Sunday_Fourth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Fifth.BackColor = schedule.Sunday_Fifth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Sixth.BackColor = schedule.Sunday_Sixth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Sunday_Seventh.BackColor = schedule.Sunday_Seventh == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_First.BackColor = schedule.Monday_First == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Second.BackColor = schedule.Monday_Second == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Third.BackColor = schedule.Monday_Third == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Fourth.BackColor = schedule.Monday_Fourth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Fifth.BackColor = schedule.Monday_Fifth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Sixth.BackColor = schedule.Monday_Sixth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Monday_Seventh.BackColor = schedule.Monday_Seventh == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_First.BackColor = schedule.Tuesday_First == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Second.BackColor = schedule.Tuesday_Second == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Third.BackColor = schedule.Tuesday_Third == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Fourth.BackColor = schedule.Tuesday_Fourth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Fifth.BackColor = schedule.Tuesday_Fifth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Sixth.BackColor = schedule.Tuesday_Sixth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Tuesday_Seventh.BackColor = schedule.Tuesday_Seventh == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_First.BackColor = schedule.Wednesday_First == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Second.BackColor = schedule.Wednesday_Second == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Third.BackColor = schedule.Wednesday_Third == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Fourth.BackColor = schedule.Wednesday_Fourth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Fifth.BackColor = schedule.Wednesday_Fifth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Sixth.BackColor = schedule.Wednesday_Sixth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Wednesday_Seventh.BackColor = schedule.Wednesday_Seventh == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_First.BackColor = schedule.Thursday_First == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Second.BackColor = schedule.Thursday_Second == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Third.BackColor = schedule.Thursday_Third == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Fourth.BackColor = schedule.Thursday_Fourth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Fifth.BackColor = schedule.Thursday_Fifth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Sixth.BackColor = schedule.Thursday_Sixth == 1 ? Color.Green : SystemColors.ControlLightLight;
            Thursday_Seventh.BackColor = schedule.Thursday_Seventh == 1 ? Color.Green : SystemColors.ControlLightLight;
        }

        private void ResetButtonColors(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                {
                    control.BackColor = SystemColors.ControlLightLight;
                }
                else if (control.HasChildren)
                {
                    ResetButtonColors(control);
                }
            }
        }

        private void Save_edit_button_Click(object sender, EventArgs e)
        {
            if (Select_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a teacher to edit.");
                return;
            }

            var selectedTeacher = Select_comboBox.SelectedItem.ToString();

            if (File.Exists(MainScheduleFile))
            {
                string json = File.ReadAllText(MainScheduleFile);
                var scheduleList = JsonConvert.DeserializeObject<List<dynamic>>(json);

                var teacherSchedule = scheduleList.FirstOrDefault(schedule => schedule.TeacherName == selectedTeacher);

                if (teacherSchedule != null)
                {
                    // Update the schedule
                    teacherSchedule.Schedule.Sunday_First = Sunday_First.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Second = Sunday_Second.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Third = Sunday_Third.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Fourth = Sunday_Fourth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Fifth = Sunday_Fifth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Sixth = Sunday_Sixth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Sunday_Seventh = Sunday_Seventh.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_First = Monday_First.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Second = Monday_Second.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Third = Monday_Third.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Fourth = Monday_Fourth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Fifth = Monday_Fifth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Sixth = Monday_Sixth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Monday_Seventh = Monday_Seventh.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_First = Tuesday_First.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Second = Tuesday_Second.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Third = Tuesday_Third.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Fourth = Tuesday_Fourth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Fifth = Tuesday_Fifth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Sixth = Tuesday_Sixth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Tuesday_Seventh = Tuesday_Seventh.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_First = Wednesday_First.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Second = Wednesday_Second.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Third = Wednesday_Third.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Fourth = Wednesday_Fourth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Fifth = Wednesday_Fifth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Sixth = Wednesday_Sixth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Wednesday_Seventh = Wednesday_Seventh.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_First = Thursday_First.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Second = Thursday_Second.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Third = Thursday_Third.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Fourth = Thursday_Fourth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Fifth = Thursday_Fifth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Sixth = Thursday_Sixth.BackColor == Color.Green ? 1 : 0;
                    teacherSchedule.Schedule.Thursday_Seventh = Thursday_Seventh.BackColor == Color.Green ? 1 : 0;

                    // Save the updated schedule back to the JSON file
                    string updatedJson = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);
                    File.WriteAllText(MainScheduleFile, updatedJson);

                    MessageBox.Show("Schedule updated successfully.");
                }
            }
        }

        private void Delete_table_button1_Click(object sender, EventArgs e)
        {
            if (Select_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a teacher to delete.");
                return;
            }

            var selectedTeacher = Select_comboBox.SelectedItem.ToString();

            if (File.Exists(MainScheduleFile))
            {
                string json = File.ReadAllText(MainScheduleFile);
                var scheduleList = JsonConvert.DeserializeObject<List<dynamic>>(json);

                var teacherSchedule = scheduleList.FirstOrDefault(schedule => schedule.TeacherName == selectedTeacher);

                if (teacherSchedule != null)
                {
                    // Remove the selected teacher's schedule
                    scheduleList.Remove(teacherSchedule);

                    // Save the updated schedule list back to the JSON file
                    string updatedJson = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);
                    File.WriteAllText(MainScheduleFile, updatedJson);

                    // Clear the selection and reset button colors
                    Select_comboBox.SelectedItem = null;
                    ResetButtonColors(this);

                    MessageBox.Show("Schedule deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Selected teacher's schedule not found.");
                }
            }
            else
            {
                MessageBox.Show("The schedule data file does not exist.");
            }
        }
    }
}
