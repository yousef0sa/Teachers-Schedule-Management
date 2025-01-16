using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Teachers__Schedule_Management.User_Control
{
    public partial class Teacher_Selection_data : UserControl
    {
        public string Teacher_name
        {
            get { return this.name_textBox.Text; }
            set { this.name_textBox.Text = value; }
        }

        public string Weekly_Reserve_times
        {
            get { return this.weekly_Reserve_times_textBox.Text; }
            set { this.weekly_Reserve_times_textBox.Text = value; }
        }

        public string Daily_Reserve_times
        {
            get { return this.daily_Reserve_times_textBox.Text; }
            set { this.daily_Reserve_times_textBox.Text = value; }
        }

        public string Weekly_Number_of_classes
        {
            get { return this.weekly_Number_of_classestext_Box.Text; }
            set { this.weekly_Number_of_classestext_Box.Text = value; }
        }

        public string Daily_Number_of_classes
        {
            get { return this.daily_Number_of_classes_textBox.Text; }
            set { this.daily_Number_of_classes_textBox.Text = value; }
        }

        public string Teacher_Status_Notification_Text
        {
            get { return this.Teacher_Status_Notification.Text; }
            set { this.Teacher_Status_Notification.Text = value; }
        }

        public object Teacher_Status_Notification_Style
        {
            get { return this.Teacher_Status_Notification.Style; }
            set { this.Teacher_Status_Notification.Style = (ReaLTaiizor.Controls.FoxNotification.Styles)value; }
        }

        public Teacher_Selection_data()
        {
            InitializeComponent();
        }

        private void Reserve_button_Click(object sender, EventArgs e)
        {
            string teacherName = this.name_textBox.Text;
            string jsonFilePath = "schedule_Reserve_Data.json";

            List<TeacherData> teachers = LoadTeacherData(jsonFilePath);

            var teacher = teachers.FirstOrDefault(t => t.TeacherName == teacherName);
            string buttonName = ((Teacher_Selection_Form)this.ParentForm).ButtonName;
            bool isRemoved = false;

            if (teacher != null)
            {
                isRemoved = UpdateTeacherSchedule(teacher, buttonName);
            }
            else
            {
                teacher = CreateNewTeacher(teacherName, buttonName);
                teachers.Add(teacher);
                this.class_textBox.ReadOnly = true;
            }

            SaveTeacherData(jsonFilePath, teachers);
            UpdateUI(isRemoved);
        }

        private List<TeacherData> LoadTeacherData(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<List<TeacherData>>(jsonData) ?? new List<TeacherData>();
            }
            return new List<TeacherData>();
        }

        private bool UpdateTeacherSchedule(TeacherData teacher, string buttonName)
        {
            bool isRemoved = false;
            if (teacher.Schedule.ContainsKey(buttonName))
            {
                if (teacher.Schedule[buttonName] == 1)
                {
                    teacher.Schedule.Remove(buttonName);
                    teacher.ClassDetails.Remove(buttonName);
                    isRemoved = true;
                    this.class_textBox.ReadOnly = false;
                    this.class_textBox.Text = string.Empty;
                }
                else
                {
                    teacher.Schedule[buttonName] = 1;
                    teacher.ClassDetails[buttonName] = this.class_textBox.Text;
                    this.class_textBox.ReadOnly = true;
                }
            }
            else
            {
                teacher.Schedule[buttonName] = 1;
                teacher.ClassDetails[buttonName] = this.class_textBox.Text;
                this.class_textBox.ReadOnly = true;
            }
            return isRemoved;
        }

        private TeacherData CreateNewTeacher(string teacherName, string buttonName)
        {
            return new TeacherData
            {
                TeacherName = teacherName,
                Schedule = new Dictionary<string, int> { { buttonName, 1 } },
                ClassDetails = new Dictionary<string, string> { { buttonName, this.class_textBox.Text } }
            };
        }

        private void SaveTeacherData(string jsonFilePath, List<TeacherData> teachers)
        {
            string updatedJsonData = JsonConvert.SerializeObject(teachers, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonData);
        }

        private void UpdateUI(bool isRemoved)
        {
            if (isRemoved)
            {
                this.Reserve_button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Info;
            }
            else
            {
                this.Reserve_button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
            }
        }

        public void UpdateButtonColor(string buttonName)
        {
            string jsonFilePath = "schedule_Reserve_Data.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                List<TeacherData> teachers = JsonConvert.DeserializeObject<List<TeacherData>>(jsonData);

                if (teachers != null)
                {
                    var teacher = teachers.FirstOrDefault(t => t.TeacherName == this.Teacher_name);
                    if (teacher != null && teacher.Schedule.ContainsKey(buttonName) && teacher.Schedule[buttonName] == 1)
                    {
                        this.Reserve_button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
                        this.class_textBox.Text = teacher.ClassDetails[buttonName];
                        this.class_textBox.ReadOnly = true;
                    }
                    else
                    {
                        this.class_textBox.ReadOnly = false;
                        this.class_textBox.Text = string.Empty;
                    }
                }
            }
        }
    }
}
