using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teachers__Schedule_Management.Class
{
    internal class UpdateHelper
    {
        private readonly string _repoOwner = "yousef0sa";
        private readonly string _repoName = "Teachers-Schedule-Management";

        public async Task CheckForUpdates()
        {
            try
            {
                // Get the current version of the application
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                // Get the latest version tag from GitHub
                var latestVersion = await GetLatestVersionFromGitHub();

                if (latestVersion != null && latestVersion > currentVersion)
                {
                    // Ask the user if they want to update
                    var result = MessageBox.Show("An update is available. Do you want to download and install it now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        await DownloadAndInstallUpdate();
                    }
                }
                else
                {
                    // Optionally inform the user that they are on the latest version
                    // MessageBox.Show("You are using the latest version.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or inform the user
                // MessageBox.Show($"An error occurred while checking for updates: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Version> GetLatestVersionFromGitHub()
        {
            try
            {
                using (var client = new WebClient())
                {
                    // Set the User-Agent header as required by GitHub API
                    client.Headers.Add("User-Agent", "Teachers-Schedule-Management-App");

                    var url = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";
                    var json = await client.DownloadStringTaskAsync(new Uri(url));

                    var release = JObject.Parse(json);
                    var latestTag = release["name"].ToString();

                    Version latestVersion;
                    if (Version.TryParse(latestTag, out latestVersion))
                    {
                        return latestVersion;
                    }
                    else if (Version.TryParse(latestTag.TrimStart('v', 'V'), out latestVersion))
                    {
                        return latestVersion;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }

            return null;
        }

        private async Task DownloadAndInstallUpdate()
        {
            try
            {
                string downloadUrl = GetInstallerDownloadUrl();
                if (string.IsNullOrEmpty(downloadUrl))
                {
                    MessageBox.Show("Unable to find the installer download URL.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tempFilePath = Path.Combine(Path.GetTempPath(), "Setup.msi");

                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Teachers-Schedule-Management-App");
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), tempFilePath);
                }

                Process.Start(tempFilePath);
                Application.Exit();
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log them or inform the user
                MessageBox.Show($"An error occurred while downloading the update: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetInstallerDownloadUrl()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Teachers-Schedule-Management-App");

                    var url = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";
                    var json = client.DownloadString(url);

                    var release = JObject.Parse(json);
                    var assets = release["assets"];
                    if (assets != null)
                    {
                        foreach (var asset in assets)
                        {
                            var name = asset["name"].ToString();
                            if (name.EndsWith(".msi"))
                            {
                                return asset["browser_download_url"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if needed
            }

            return null;
        }
    }
}
