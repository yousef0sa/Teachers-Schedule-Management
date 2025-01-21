using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Teachers__Schedule_Management.User_Control
{
    public partial class Schedule_Add_v0 : UserControl
    {
        private static readonly string MainScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scheduleData.json");
        public Schedule_Add_v0()
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

        private void Save_button_Click_1(object sender, EventArgs e)
        {
            var newScheduleData = new
            {
                TeacherName = Teacher_name_textBox.Text,
                Schedule = new Dictionary<string, int>
                {
                    { "Sunday_First", Sunday_First.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Second", Sunday_Second.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Third", Sunday_Third.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Fourth", Sunday_Fourth.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Fifth", Sunday_Fifth.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Sixth", Sunday_Sixth.BackColor == Color.Green ? 1 : 0 },
                    { "Sunday_Seventh", Sunday_Seventh.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_First", Monday_First.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Second", Monday_Second.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Third", Monday_Third.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Fourth", Monday_Fourth.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Fifth", Monday_Fifth.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Sixth", Monday_Sixth.BackColor == Color.Green ? 1 : 0 },
                    { "Monday_Seventh", Monday_Seventh.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_First", Tuesday_First.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Second", Tuesday_Second.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Third", Tuesday_Third.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Fourth", Tuesday_Fourth.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Fifth", Tuesday_Fifth.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Sixth", Tuesday_Sixth.BackColor == Color.Green ? 1 : 0 },
                    { "Tuesday_Seventh", Tuesday_Seventh.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_First", Wednesday_First.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Second", Wednesday_Second.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Third", Wednesday_Third.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Fourth", Wednesday_Fourth.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Fifth", Wednesday_Fifth.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Sixth", Wednesday_Sixth.BackColor == Color.Green ? 1 : 0 },
                    { "Wednesday_Seventh", Wednesday_Seventh.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_First", Thursday_First.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Second", Thursday_Second.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Third", Thursday_Third.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Fourth", Thursday_Fourth.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Fifth", Thursday_Fifth.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Sixth", Thursday_Sixth.BackColor == Color.Green ? 1 : 0 },
                    { "Thursday_Seventh", Thursday_Seventh.BackColor == Color.Green ? 1 : 0 }
                }
            };

            List<object> scheduleList = new List<object>();

            if (File.Exists(MainScheduleFile))
            {
                string existingJson = File.ReadAllText(MainScheduleFile);

                // Check if the existing JSON is an array or an object
                if (existingJson.Trim().StartsWith("["))
                {
                    scheduleList = JsonConvert.DeserializeObject<List<object>>(existingJson) ?? new List<object>();
                }
                else
                {
                    var singleObject = JsonConvert.DeserializeObject<object>(existingJson);
                    scheduleList.Add(singleObject);
                }
            }

            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(Teacher_name_textBox.Text))
            {
                MessageBox.Show("Please enter a teacher name.");
                return;
            }

            // Check if the TeacherName already exists
            bool teacherExists = scheduleList.Any(schedule =>
                schedule != null && ((dynamic)schedule).TeacherName == newScheduleData.TeacherName);

            if (teacherExists)
            {
                MessageBox.Show("Teacher name already exists. Please enter a unique name.");
                return;
            }

            scheduleList.Add(newScheduleData);

            string newJson = JsonConvert.SerializeObject(scheduleList, Formatting.Indented);
            File.WriteAllText(MainScheduleFile, newJson);
            MessageBox.Show("Data saved successfully!");
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
        private void Cancel_button_Click_1(object sender, EventArgs e)
        {
            // Clean everything
            Teacher_name_textBox.Text = "";
            ResetButtonColors(this);
        }

    }
}
