using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teachers__Schedule_Management.User_Control
{
    public class TeacherDataLoader
    {
        public List<int> LoadAvailableTeachers()
        {
            string jsonFilePath = "scheduleData.json";
            string reserveJsonFilePath = "schedule_Reserve_Data.json";

            if (!File.Exists(jsonFilePath))
            {
                ShowErrorMessage("No data.", "Missing file");
                return new List<int>();
            }

            string jsonData = File.ReadAllText(jsonFilePath);
            string reserveJsonData = File.Exists(reserveJsonFilePath) ? File.ReadAllText(reserveJsonFilePath) : string.Empty;

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                ShowErrorMessage("No data.", "Empty");
                return new List<int>();
            }

            List<TeacherData> teachers = DeserializeJsonData<List<TeacherData>>(jsonData);
            List<TeacherData> reserveTeachers = !string.IsNullOrWhiteSpace(reserveJsonData)
                ? DeserializeJsonData<List<TeacherData>>(reserveJsonData)
                : new List<TeacherData>();

            if (teachers == null)
            {
                ShowErrorMessage("Failed to deserialize the main JSON file.", "Error");
                return new List<int>();
            }

            var weeklyReserveData = GetWeeklyReserveData(reserveTeachers);
            return weeklyReserveData;
        }

        private void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private T DeserializeJsonData<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        private List<int> GetWeeklyReserveData(List<TeacherData> reserveTeachers)
        {
            var weeklyReserveData = new List<int>(new int[5]); // 5 أيام في الأسبوع

            foreach (var teacher in reserveTeachers)
            {
                foreach (var schedule in teacher.Schedule)
                {
                    string day = schedule.Key.Split('_')[0];
                    int dayIndex = GetDayIndex(day);
                    if (dayIndex != -1)
                    {
                        weeklyReserveData[dayIndex] += schedule.Value;
                    }
                }
            }

            return weeklyReserveData;
        }

        private int GetDayIndex(string day)
        {
            switch (day.ToLower())
            {
                case "sunday": return 0;
                case "monday": return 1;
                case "tuesday": return 2;
                case "wednesday": return 3;
                case "thursday": return 4;
                default: return -1;
            }
        }
    }
}
