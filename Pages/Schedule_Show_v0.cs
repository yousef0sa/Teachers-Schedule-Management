using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Teachers__Schedule_Management.User_Control;

namespace Teachers__Schedule_Management.NewFolder1
{
    public partial class Schedule_Show_v0 : UserControl
    {
        private Label[] dayLabels;

        public Schedule_Show_v0()
        {
            InitializeComponent();
            InitializeDayLabels();
            UpdateLabelColors();
            this.Dock = DockStyle.Fill;
            AddButtonClickEvents(); // Add this line to initialize button click events
        }

        private void InitializeDayLabels()
        {
            // استخدم الـ Labels الموجودة بالفعل
            dayLabels = new Label[5];
            dayLabels[0] = Sunday_label;
            dayLabels[1] = Monday_label;
            dayLabels[2] = Tuesday_label;
            dayLabels[3] = Wednesday_label;
            dayLabels[4] = Thursday_label;
        }

        private void UpdateLabelColors()
        {
            DayOfWeek today = DateTime.Now.DayOfWeek;
            Console.Write(today);
            foreach (Label label in dayLabels)
            {
                if (label.Text == today.ToString())
                {
                    label.BackColor = Color.BurlyWood; // لون الخلفية لليوم الحالي
                }
                else
                {
                    label.BackColor = Color.Transparent; // لون الخلفية للأيام الأخرى
                }
            }
            UpdateButtonColors(); // تحديث ألوان الأزرار
        }

        private void AddButtonClickEvents()
        {
            AddButtonClickEventsRecursive(this);
        }

        private void AddButtonClickEventsRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                {
                    control.Click += OpenTeacherSelectionForm;
                }
                else if (control.HasChildren)
                {
                    AddButtonClickEventsRecursive(control);
                }
            }
        }

        private void OpenTeacherSelectionForm(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Teacher_Selection_Form form = new Teacher_Selection_Form
                {
                    ButtonName = clickedButton.Name
                };
                form.ShowDialog();
                UpdateButtonColors(); // تحديث الألوان بعد إغلاق النموذج
            }
        }
        private void UpdateButtonColors()
        {
            string jsonFilePath = "schedule_Reserve_Data.json"; // ضع مسار ملف JSON هنا

            if (File.Exists(jsonFilePath))
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                List<TeacherData> teachers = JsonConvert.DeserializeObject<List<TeacherData>>(jsonData);

                if (teachers != null)
                {
                    UpdateButtonColorsRecursive(this, teachers);
                }
            }
        }
        private void UpdateButtonColorsRecursive(Control parent, List<TeacherData> teachers)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    bool isReserved = false;
                    foreach (var teacher in teachers)
                    {
                        if (teacher.Schedule.ContainsKey(button.Name) && teacher.Schedule[button.Name] == 1)
                        {
                            button.BackColor = Color.Green; // لون مميز للزر المحجوز
                            isReserved = true;
                            break;
                        }
                    }
                    if (!isReserved)
                    {
                        button.BackColor = SystemColors.ControlLightLight; // لون افتراضي للزر
                    }
                }
                else if (control.HasChildren)
                {
                    UpdateButtonColorsRecursive(control, teachers);
                }
            }
        }
    }
}
