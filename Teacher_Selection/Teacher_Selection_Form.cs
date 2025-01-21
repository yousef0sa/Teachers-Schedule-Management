using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Teachers__Schedule_Management.User_Control
{
    public partial class Teacher_Selection_Form : Form
    {
        private static readonly string MainScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scheduleData.json");
        private static readonly string ReserveScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "schedule_Reserve_Data.json");
        private static readonly string LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_Reserve.json");

        public string ButtonName { get; set; }

        public Teacher_Selection_Form()
        {
            InitializeComponent();
        }

        private void Teacher_Selection_Form_Load(object sender, EventArgs e)
        {
            LoadAvailableTeachers();
        }

        private void LoadAvailableTeachers()
        {
            if (!File.Exists(MainScheduleFile))
            {
                ShowErrorMessage("No data.", "Missing file");
                return;
            }

            var teachers = DeserializeJsonData<List<TeacherData>>(MainScheduleFile);
            if (teachers == null || !teachers.Any())
            {
                ShowErrorMessage("Failed to load teacher data.", "Error");
                return;
            }

            var reserveTeachers = LoadReserveTeachers();
            var availableTeachers = GetAvailableTeachers(teachers);
            var teacherDataList = CreateTeacherDataList(availableTeachers, reserveTeachers);

            AddTeacherDataToPanel(teacherDataList);
        }

        private List<TeacherData> LoadReserveTeachers()
        {
            if (!File.Exists(ReserveScheduleFile))
                return new List<TeacherData>();

            return DeserializeJsonData<List<TeacherData>>(ReserveScheduleFile) ?? new List<TeacherData>();
        }

        private void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private T DeserializeJsonData<T>(string filePath) where T : class
        {
            try
            {
                var jsonData = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(jsonData))
                    return null;

                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error reading {filePath}: {ex.Message}", "Error");
                return null;
            }
        }

        private List<TeacherData> GetAvailableTeachers(List<TeacherData> teachers)
        {
            return teachers
                .Where(t => t.Schedule.ContainsKey(ButtonName) && t.Schedule[ButtonName] == 0)
                .ToList();
        }

        private List<Teacher_Selection_data> CreateTeacherDataList(
            List<TeacherData> availableTeachers,
            List<TeacherData> reserveTeachers)
        {
            var teacherDataList = new List<Teacher_Selection_data>();

            foreach (var teacher in availableTeachers)
            {
                int weeklyReserveTimes = CalculateWeeklyReserveTimes(reserveTeachers, teacher.TeacherName);
                int dailyReserveTimes = CalculateDailyReserveTimes(reserveTeachers, teacher.TeacherName);
                int weeklyNumberOfClasses = CalculateWeeklyNumberOfClasses(teacher);
                int dailyNumberOfClasses = CalculateDailyNumberOfClasses(teacher);

                var teacherData = new Teacher_Selection_data
                {
                    Teacher_name = teacher.TeacherName,
                    Weekly_Reserve_times = weeklyReserveTimes.ToString(),
                    Daily_Reserve_times = dailyReserveTimes.ToString(),
                    Weekly_Number_of_classes = weeklyNumberOfClasses.ToString(),
                    Daily_Number_of_classes = dailyNumberOfClasses.ToString(),
                };

                teacherData.Dock = DockStyle.Top;
                teacherData.UpdateButtonColor(ButtonName);
                UpdateTeacherStatus(teacherData);

                teacherDataList.Add(teacherData);
            }

            return teacherDataList
                .OrderBy(td => int.Parse(td.Weekly_Number_of_classes) + int.Parse(td.Weekly_Reserve_times))
                .ToList();
        }

        private int CalculateWeeklyReserveTimes(List<TeacherData> reserveTeachers, string teacherName)
        {
            return reserveTeachers
                .Where(rt => rt.TeacherName == teacherName)
                .Sum(rt => rt.Schedule.Count);
        }

        private int CalculateDailyReserveTimes(List<TeacherData> reserveTeachers, string teacherName)
        {
            return reserveTeachers
                .Where(rt => rt.TeacherName == teacherName && rt.Schedule.ContainsKey(ButtonName))
                .Sum(rt => rt.Schedule[ButtonName]);
        }

        private int CalculateWeeklyNumberOfClasses(TeacherData teacher)
        {
            return teacher.Schedule.Values.Count(value => value == 1);
        }

        private int CalculateDailyNumberOfClasses(TeacherData teacher)
        {
            string dayPrefix = ButtonName.Split('_')[0];
            return teacher.Schedule
                .Where(s => s.Key.StartsWith(dayPrefix) && s.Value == 1)
                .Count();
        }

        private void AddTeacherDataToPanel(List<Teacher_Selection_data> teacherDataList)
        {
            foreach (var teacherData in teacherDataList)
            {
                Main_panel1.Controls.Add(teacherData);
            }
        }

        private void UpdateTeacherStatus(Teacher_Selection_data teacherData)
        {
            int totalPoints = int.Parse(teacherData.Weekly_Number_of_classes) + int.Parse(teacherData.Weekly_Reserve_times);
            int repeatCount = GetRepeatCount(teacherData.Teacher_name, ButtonName);

            if (repeatCount > 0)
            {
                teacherData.Teacher_Status_Notification_Text = $"Status: Repeat {repeatCount}";
                teacherData.Teacher_Status_Notification_Style = ReaLTaiizor.Controls.FoxNotification.Styles.Yellow;
            }
            else if (totalPoints >= 24)
            {
                teacherData.Teacher_Status_Notification_Text = "Status: Limit Reached";
                teacherData.Teacher_Status_Notification_Style = ReaLTaiizor.Controls.FoxNotification.Styles.Red;
            }
            else
            {
                teacherData.Teacher_Status_Notification_Text = "Status: Excellent";
                teacherData.Teacher_Status_Notification_Style = ReaLTaiizor.Controls.FoxNotification.Styles.Green;
            }
        }

        private int GetRepeatCount(string teacherName, string buttonName)
        {
            if (!File.Exists(LogFile))
                return 0;

            try
            {
                string logJson = File.ReadAllText(LogFile);
                var logData = JsonConvert.DeserializeObject<Dictionary<string, List<TeacherData>>>(logJson);
                if (logData == null)
                    return 0;

                return logData.Values
                    .SelectMany(teacherList => teacherList)
                    .Count(t => t.TeacherName == teacherName
                             && t.Schedule.ContainsKey(buttonName)
                             && t.Schedule[buttonName] == 1);
            }
            catch (Exception)
            {
                // Log the exception if necessary
                return 0;
            }
        }
    }
}
