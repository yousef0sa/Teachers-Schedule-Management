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

        private void Teacher_Selection_Form_Load(object sender, EventArgs e)
        {
            LoadAvailableTeachers();
        }

        private void LoadAvailableTeachers()
        {
            string jsonFilePath = "scheduleData.json";
            string reserveJsonFilePath = "schedule_Reserve_Data.json";

            if (!File.Exists(jsonFilePath))
            {
                ShowErrorMessage("No data.", "Missing file");
                return;
            }

            string jsonData = File.ReadAllText(jsonFilePath);
            string reserveJsonData = File.Exists(reserveJsonFilePath) ? File.ReadAllText(reserveJsonFilePath) : string.Empty;

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                ShowErrorMessage("No data.", "Empty");
                return;
            }

            List<TeacherData> teachers = DeserializeJsonData<List<TeacherData>>(jsonData);
            List<TeacherData> reserveTeachers = !string.IsNullOrWhiteSpace(reserveJsonData)
                ? DeserializeJsonData<List<TeacherData>>(reserveJsonData)
                : new List<TeacherData>();

            if (teachers == null)
            {
                ShowErrorMessage("Failed to deserialize the main JSON file.", "Error");
                return;
            }

            var availableTeachers = GetAvailableTeachers(teachers);
            var teacherDataList = CreateTeacherDataList(availableTeachers, reserveTeachers);

            AddTeacherDataToPanel(teacherDataList);
        }

        private void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private T DeserializeJsonData<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        private List<TeacherData> GetAvailableTeachers(List<TeacherData> teachers)
        {
            return teachers.Where(t => t.Schedule.ContainsKey(ButtonName) && t.Schedule[ButtonName] == 0).ToList();
        }

        private List<Teacher_Selection_data> CreateTeacherDataList(List<TeacherData> availableTeachers, List<TeacherData> reserveTeachers)
        {
            var teacherDataList = new List<Teacher_Selection_data>();

            foreach (var teacher in availableTeachers)
            {
                int weeklyReserveTimes = CalculateWeeklyReserveTimes(reserveTeachers, teacher.TeacherName);
                int dailyReserveTimes = CalculateDailyReserveTimes(reserveTeachers, teacher.TeacherName);
                int weeklyNumberOfClasses = CalculateWeeklyNumberOfClasses(teacher);
                int dailyNumberOfClasses = CalculateDailyNumberOfClasses(teacher);

                Teacher_Selection_data teacherData = new Teacher_Selection_data
                {
                    Teacher_name = teacher.TeacherName,
                    Weekly_Reserve_times = weeklyReserveTimes.ToString(),
                    Daily_Reserve_times = dailyReserveTimes.ToString(),
                    Weekly_Number_of_classes = weeklyNumberOfClasses.ToString(),
                    Daily_Number_of_classes = dailyNumberOfClasses.ToString(),
                };

                teacherData.UpdateButtonColor(ButtonName);
                Update_Teacher_Status(teacherData);

                teacherDataList.Add(teacherData);
            }

            return teacherDataList.OrderBy(td => int.Parse(td.Weekly_Number_of_classes) + int.Parse(td.Weekly_Reserve_times)).ToList();
        }

        private int CalculateWeeklyReserveTimes(List<TeacherData> reserveTeachers, string teacherName)
        {
            return reserveTeachers.Where(rt => rt.TeacherName == teacherName).Sum(rt => rt.Schedule.Count);
        }

        private int CalculateDailyReserveTimes(List<TeacherData> reserveTeachers, string teacherName)
        {
            return reserveTeachers.Where(rt => rt.TeacherName == teacherName && rt.Schedule.ContainsKey(ButtonName)).Sum(rt => rt.Schedule[ButtonName]);
        }

        private int CalculateWeeklyNumberOfClasses(TeacherData teacher)
        {
            return teacher.Schedule.Count(s => s.Value == 1);
        }

        private int CalculateDailyNumberOfClasses(TeacherData teacher)
        {
            string dayPrefix = ButtonName.Split('_')[0];
            return teacher.Schedule.Where(s => s.Key.StartsWith(dayPrefix) && s.Value == 1).Count();
        }

        private void AddTeacherDataToPanel(List<Teacher_Selection_data> teacherDataList)
        {
            foreach (var teacherData in teacherDataList)
            {
                teacherData.Dock = DockStyle.Top;
                Main_panel1.Controls.Add(teacherData);
            }
        }

        private void Update_Teacher_Status(Teacher_Selection_data teacherData)
        {
            int totalPoints = int.Parse(teacherData.Weekly_Number_of_classes) + int.Parse(teacherData.Weekly_Reserve_times);
            if (totalPoints >= 24)
            {
                teacherData.Teacher_Status_Notification_Text = "الحالة: مضغوط";
                teacherData.Teacher_Status_Notification_Style = ReaLTaiizor.Controls.FoxNotification.Styles.Red;
            }
            else
            {
                teacherData.Teacher_Status_Notification_Text = "الحالة: ممتاز";
                teacherData.Teacher_Status_Notification_Style = ReaLTaiizor.Controls.FoxNotification.Styles.Green;
            }
        }
    }

}
