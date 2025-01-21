using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Teachers__Schedule_Management.User_Control;

namespace Teachers__Schedule_Management.Class
{
    public class ReserveLogManager
    {
        private static readonly string _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_Reserve.json");
        private readonly string[] _daysOfWeek = { "sunday", "monday", "tuesday", "wednesday", "thursday" };

        public void CreateReserveLog(List<TeacherData> reserveTeachers)
        {
            var daysOfWeek = _daysOfWeek;
            var dateRangeKey = GetDateRangeKey();

            var filteredReserveData = reserveTeachers.Select(teacher => new TeacherData
            {
                TeacherName = teacher.TeacherName,
                Schedule = teacher.Schedule
                    .Where(s => daysOfWeek.Contains(s.Key.Split('_')[0].ToLower()))
                    .ToDictionary(s => s.Key, s => s.Value),
                ClassDetails = teacher.ClassDetails
            }).ToList();

            var logData = new Dictionary<string, List<TeacherData>>();

            // Load existing log data if the file exists
            if (File.Exists(_logFilePath))
            {
                string existingJson = File.ReadAllText(_logFilePath);
                logData = JsonConvert.DeserializeObject<Dictionary<string, List<TeacherData>>>(existingJson) ?? new Dictionary<string, List<TeacherData>>();
            }

            // Add or update the log data with the new entry
            if (logData.ContainsKey(dateRangeKey))
            {
                logData[dateRangeKey].AddRange(filteredReserveData);
            }
            else
            {
                logData[dateRangeKey] = filteredReserveData;
            }

            string json = JsonConvert.SerializeObject(logData, Formatting.Indented);
            File.WriteAllText(_logFilePath, json);
        }


        private string GetDateRangeKey()
        {
            // Use GregorianCalendar to ensure dates are in Gregorian calendar
            var gregorianCalendar = new GregorianCalendar();

            // Get the current date in Gregorian calendar
            var now = DateTime.Now;

            // Calculate the difference to Sunday (assuming Sunday is the first day of the week)
            int diffToSunday = ((int)gregorianCalendar.GetDayOfWeek(now) + 6) % 7;
            var sunday = gregorianCalendar.AddDays(now.Date, -diffToSunday);
            var wednesday = gregorianCalendar.AddDays(sunday, 3);

            // Format the date range using Gregorian calendar
            string dateRangeKey = $"{sunday.ToString("dd", CultureInfo.InvariantCulture)}-{wednesday.ToString("dd", CultureInfo.InvariantCulture)}/{wednesday.ToString("MM/yyyy", CultureInfo.InvariantCulture)}";
            return dateRangeKey;
        }
    }
}
