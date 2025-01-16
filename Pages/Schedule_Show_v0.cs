using Newtonsoft.Json;
using ReaLTaiizor.Controls;
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
            AddButtonClickEvents();
        }

        private void InitializeDayLabels()
        {
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
            foreach (Label label in dayLabels)
            {
                if (label.Text == today.ToString())
                {
                    label.BackColor = Color.BurlyWood;
                }
                else
                {
                    label.BackColor = Color.Transparent;
                }
            }
            UpdateButtonColors();
        }

        private void AddButtonClickEvents()
        {
            AddButtonClickEventsRecursive(this);
        }

        private void AddButtonClickEventsRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is HopeButton)
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
            HopeButton clickedButton = sender as HopeButton;
            if (clickedButton != null)
            {
                Teacher_Selection_Form form = new Teacher_Selection_Form
                {
                    ButtonName = clickedButton.Name
                };
                form.ShowDialog();
                UpdateButtonColors();
            }
        }

        private void UpdateButtonColors()
        {
            string jsonFilePath = "schedule_Reserve_Data.json";

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
                if (control is HopeButton button)
                {
                    bool isReserved = false;
                    foreach (var teacher in teachers)
                    {
                        if (teacher.Schedule.ContainsKey(button.Name) && teacher.Schedule[button.Name] == 1)
                        {
                            button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Success;
                            isReserved = true;
                            break;
                        }
                    }
                    if (!isReserved)
                    {
                        button.ButtonType = ReaLTaiizor.Util.HopeButtonType.Default;
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
