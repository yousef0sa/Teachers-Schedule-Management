using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Teachers__Schedule_Management.Class;

namespace Teachers__Schedule_Management.User_Control
{
    public class TeacherDataLoader
    {
        private static readonly string MainScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scheduleData.json");
        private static readonly string ReserveScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "schedule_Reserve_Data.json");
        private static readonly string LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_Reserve.json");

        public List<int> LoadAvailableTeachers()
        {
            if (!File.Exists(MainScheduleFile))
            {
                ShowErrorMessage("No data.", "Missing file");
                return new List<int>();
            }

            string jsonData = File.ReadAllText(MainScheduleFile);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                ShowErrorMessage("No data.", "Empty");
                return new List<int>();
            }

            var teachers = DeserializeJsonData<List<TeacherData>>(jsonData);
            if (teachers == null)
            {
                ShowErrorMessage("Failed to deserialize the main JSON file.", "Error");
                return new List<int>();
            }

            var reserveTeachers = LoadReserveTeachers();
            var weeklyReserveData = GetWeeklyReserveData(reserveTeachers);

            return weeklyReserveData;
        }

        private List<TeacherData> LoadReserveTeachers()
        {
            if (!File.Exists(ReserveScheduleFile))
                return new List<TeacherData>();

            string reserveJsonData = File.ReadAllText(ReserveScheduleFile);
            if (string.IsNullOrWhiteSpace(reserveJsonData))
                return new List<TeacherData>();

            return DeserializeJsonData<List<TeacherData>>(reserveJsonData);
        }

        private void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private T DeserializeJsonData<T>(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (JsonException ex)
            {
                ShowErrorMessage($"Deserialization error: {ex.Message}", "Error");
                return default;
            }
        }

        private List<int> GetWeeklyReserveData(List<TeacherData> reserveTeachers)
        {
            var weeklyReserveData = new int[5]; // 5 days in a week

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

            return weeklyReserveData.ToList();
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

        public bool DeleteReserveRecord()
        {
            if (!File.Exists(ReserveScheduleFile))
            {
                ShowErrorMessage("No reserve data to delete.", "Information");
                return false;
            }

            // Read the reserve data
            string reserveJsonData = File.ReadAllText(ReserveScheduleFile);
            var reserveTeachers = DeserializeJsonData<List<TeacherData>>(reserveJsonData) ?? new List<TeacherData>();

            // Create the reserve log before deletion
            var reserveLogManager = new ReserveLogManager();
            reserveLogManager.CreateReserveLog(reserveTeachers);

            // Delete the reserve data file
            File.Delete(ReserveScheduleFile);
            return true;
        }

        public bool DeleteMonthlyLog()
        {
            if (!File.Exists(LogFile))
            {
                ShowErrorMessage("No monthly log to delete.", "Information");
                return false;
            }
            File.Delete(LogFile);
            return true;
        }
    }
}
