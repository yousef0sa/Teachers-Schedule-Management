using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Teachers__Schedule_Management.Class
{
    internal class UpdateUI
    {
        public void UpdateProgramVersionLabel(Label programVersionLabel)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            programVersionLabel.Text = $"Version: {version}";
        }
        
    }
}
