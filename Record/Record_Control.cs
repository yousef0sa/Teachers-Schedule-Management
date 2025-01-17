using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teachers__Schedule_Management.User_Control
{
    public partial class _Record_Control : UserControl
    {
        public string Teacher_name
        {
            get { return this.name_textBox.Text; }
            set { this.name_textBox.Text = value; }
        }
        public string Day
        {
            get { return this.Day_textBox.Text; }
            set { this.Day_textBox.Text = value; }
        }
        public string When_Class
        {
            get { return this.When_class_textBox.Text; }
            set { this.When_class_textBox.Text = value; }
        }
        public string Where_Class
        {
            get { return this.Where_class_textBox.Text; }
            set
            {
                this.Where_class_textBox.Text = value;
                this.Where_class_textBox.RightToLeft = IsArabic(value) ? RightToLeft.Yes : RightToLeft.No;
            }
        }
        public _Record_Control()
        {
            InitializeComponent();
        }

        private bool IsArabic(string text)
        {
            return text.Any(c => c >= 0x0600 && c <= 0x06FF);
        }
    }
}
