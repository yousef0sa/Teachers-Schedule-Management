using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;


namespace Teachers__Schedule_Management.Class
{
    internal class Update
    {
        private string _updateUrl = "https://github.com/yousef0sa/Teachers-Schedule-Management/releases/latest";
        public void UpdateProgramVersionLabel(Label programVersionLabel)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            programVersionLabel.Text = $"Version: {version}";
        }

        public async void CheckForUpdates()
        {
            try
            {
                using (var manager = new UpdateManager(_updateUrl))
                {
                    await manager.UpdateApp();
                    MessageBox.Show("The application has been updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
