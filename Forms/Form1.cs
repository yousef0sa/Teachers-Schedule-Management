using System;
using System.Windows.Forms;
using Teachers__Schedule_Management.Forms;
using Teachers__Schedule_Management.NewFolder1;
using Teachers__Schedule_Management.User_Control;

namespace Teachers__Schedule_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Schedule_button_Click(object sender, EventArgs e)
        {
            // lod the Schedule to Page1_panel
            Page1_panel.Controls.Clear();
            Schedule_Show_v0 schedule = new Schedule_Show_v0();
            Page1_panel.Controls.Add(schedule);

        }

        private void Add_schedule_button_Click(object sender, EventArgs e)
        {
            Page1_panel.Controls.Clear();
            Schedule_Add_v0 schedule = new Schedule_Add_v0();
            Page1_panel.Controls.Add(schedule);

        }

        private void Edit_Delete_button_Click(object sender, EventArgs e)
        {
            Page1_panel.Controls.Clear();
            Schedule_Edit_Delete_v0 schedule = new Schedule_Edit_Delete_v0();
            Page1_panel.Controls.Add(schedule);
        }

        private void Record_button_Click(object sender, EventArgs e)
        {
            Record_Form recordForm = new Record_Form();
            recordForm.ShowDialog();
        }

        private void Delete_record_button_Click(object sender, EventArgs e)
        {
            // Delete old data
            string jsonFilePath = "schedule_Reserve_Data.json";
            if (System.IO.File.Exists(jsonFilePath))
            {
                if (MessageBox.Show("Are you sure you want to delete the data?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.IO.File.Delete(jsonFilePath);
                }
            }
            else
            {
                MessageBox.Show("No data to delete.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
