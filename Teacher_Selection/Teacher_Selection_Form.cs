using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Teachers__Schedule_Management.User_Control
{
    public partial class Teacher_Selection_Form : Form
    {
        public string ButtonName { get; set; }

        public Teacher_Selection_Form()
        {
            InitializeComponent();
        }

        private void LoadAvailableTeachers()
        {
            string jsonFilePath = "scheduleData.json";
            string reserveJsonFilePath = "schedule_Reserve_Data.json"; // Path to the reserve JSON file

            if (!File.Exists(jsonFilePath))
            {
                MessageBox.Show("No data.", "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string jsonData = File.ReadAllText(jsonFilePath);
            string reserveJsonData = File.Exists(reserveJsonFilePath) ? File.ReadAllText(reserveJsonFilePath) : string.Empty;

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                MessageBox.Show("No data.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<TeacherData> teachers = JsonConvert.DeserializeObject<List<TeacherData>>(jsonData);
            List<TeacherData> reserveTeachers = !string.IsNullOrWhiteSpace(reserveJsonData)
                ? JsonConvert.DeserializeObject<List<TeacherData>>(reserveJsonData)
                : new List<TeacherData>();

            if (teachers == null)
            {
                MessageBox.Show("Failed to deserialize the main JSON file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var availableTeachers = teachers.Where(t => t.Schedule.ContainsKey(ButtonName) && t.Schedule[ButtonName] == 0).ToList();

            var teacherDataList = new List<Teacher_Selection_data>();

            foreach (var teacher in availableTeachers)
            {
                int weeklyReserveTimes = reserveTeachers
                    .Where(rt => rt.TeacherName == teacher.TeacherName)
                    .Sum(rt => rt.Schedule.Count);

                int dailyReserveTimes = reserveTeachers
                    .Where(rt => rt.TeacherName == teacher.TeacherName && rt.Schedule.ContainsKey(ButtonName))
                    .Sum(rt => rt.Schedule[ButtonName]);

                int weeklyNumberOfClasses = teacher.Schedule.Count(s => s.Value == 1);
                int dailyNumberOfClasses = teacher.Schedule.ContainsKey(ButtonName) && teacher.Schedule[ButtonName] == 1 ? 1 : 0;

                // Calculate daily classes and daily reserves
                string dayPrefix = ButtonName.Split('_')[0];
                int dailyClasses = teacher.Schedule
                    .Where(s => s.Key.StartsWith(dayPrefix) && s.Value == 1)
                    .Count();

                int dailyReserves = reserveTeachers
                    .Where(rt => rt.TeacherName == teacher.TeacherName)
                    .Sum(rt => rt.Schedule.Count(s => s.Key.StartsWith(dayPrefix) && s.Value == 1));

                Teacher_Selection_data teacherData = new Teacher_Selection_data
                {
                    Teacher_name = teacher.TeacherName,
                    Weekly_Reserve_times = weeklyReserveTimes.ToString(),
                    Daily_Reserve_times = dailyReserves.ToString(),
                    Weekly_Number_of_classes = weeklyNumberOfClasses.ToString(),
                    Daily_Number_of_classes = dailyClasses.ToString(),
                };

                teacherData.UpdateButtonColor(ButtonName); // Call the method to update the button color

                teacherDataList.Add(teacherData);
            }

            // Sort teachers by total points so that teachers with the highest total are loaded first
            var sortedTeacherDataList = teacherDataList
                .OrderByDescending(td => int.Parse(td.Daily_Number_of_classes) + int.Parse(td.Weekly_Reserve_times))
                .ToList();

            foreach (var teacherData in sortedTeacherDataList)
            {
                teacherData.Dock = DockStyle.Top;
                Main_panel1.Controls.Add(teacherData);
            }
        }

        private void Teacher_Selection_Form_Load(object sender, EventArgs e)
        {
            LoadAvailableTeachers();
        }
    }

}
