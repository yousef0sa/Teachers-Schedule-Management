using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teachers__Schedule_Management.User_Control
{
    public class TeacherData
    {
        public string TeacherName { get; set; }
        public Dictionary<string, int> Schedule { get; set; }
        public Dictionary<string, string> ClassDetails { get; set; }
    }
}

