using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Teachers__Schedule_Management.User_Control;

namespace Teachers__Schedule_Management.Forms
{
    public partial class Record_Form : Form
    {
        private static readonly string ReserveScheduleFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "schedule_Reserve_Data.json");
        public Record_Form()
        {
            InitializeComponent();
        }

        private void Record_Form_Load(object sender, EventArgs e)
        {
            LoadTeacherRecords();
        }

        private void LoadTeacherRecords()
        {

            if (!File.Exists(ReserveScheduleFile))
            {
                return;
            }

            string jsonData = File.ReadAllText(ReserveScheduleFile);
            List<TeacherData> teachers = JsonConvert.DeserializeObject<List<TeacherData>>(jsonData);

            if (teachers == null)
            {
                return;
            }

            var dayOrder = new Dictionary<string, int>
                    {
                        { "Sunday", 0 },
                        { "Monday", 1 },
                        { "Tuesday", 2 },
                        { "Wednesday", 3 },
                        { "Thursday", 4 },
                    };

            var classOrder = new Dictionary<string, int>
                    {
                        { "First", 0 },
                        { "Second", 1 },
                        { "Third", 2 },
                        { "Fourth", 3 },
                        { "Fifth", 4 },
                        { "Sixth", 5 },
                        { "Seventh", 6 },
                    };

            var sortedSchedules = teachers.SelectMany(teacher => teacher.Schedule.Select(schedule => new
            {
                TeacherName = teacher.TeacherName,
                Day = schedule.Key.Split('_')[0], // Assuming the key format is "Day_Class"
                WhenClass = schedule.Key.Split('_')[1],
                WhereClass = teacher.ClassDetails[schedule.Key]
            }))
            .OrderBy(record => dayOrder.ContainsKey(record.Day) ? dayOrder[record.Day] : int.MaxValue)
            .ThenBy(record => classOrder.ContainsKey(record.WhenClass) ? classOrder[record.WhenClass] : int.MaxValue)
            .ToList();

            foreach (var record in sortedSchedules)
            {
                var recordControl = new _Record_Control
                {
                    Teacher_name = record.TeacherName,
                    Day = record.Day,
                    When_Class = record.WhenClass,
                    Where_Class = record.WhereClass
                };

                // Add the control to the form
                Record_data_flowLayoutPanel.Controls.Add(recordControl);
            }
        }
    }
}
