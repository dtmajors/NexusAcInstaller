using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace NexusAsheronsCallInstaller
{
    public partial class MainWindow : Window
    {
        private List<InstallItem> _installItems = new List<InstallItem>();
        private string _saveStateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "install_state.json");

        public MainWindow()
        {
            InitializeComponent();
            LoadInstallState();
            Checklist.ItemsSource = _installItems;
        }

        private void LoadInstallState()
        {
            if (File.Exists(_saveStateFile))
            {
                var json = File.ReadAllText(_saveStateFile);
                var savedItems = JsonSerializer.Deserialize<List<InstallItem>>(json);
                if (savedItems != null)
                {
                    _installItems = savedItems;
                    return;
                }
            }

            _installItems = new List<InstallItem>
            {
                new InstallItem { Name = "Asheron's Call Client", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Client Update", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Visual Studio Runtimes", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = ".NET Frameworks", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Decal", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Thwarg Launcher", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Virindi Tank", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "MagTools", Status = InstallStatus.NotStarted, IsSelected = true },
                new InstallItem { Name = "Utility Belt", Status = InstallStatus.NotStarted, IsSelected = true }
            };
        }

        private void SaveInstallState()
        {
            var json = JsonSerializer.Serialize(_installItems);
            File.WriteAllText(_saveStateFile, json);
        }

        private async void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            if (InstallButton.Content.ToString() == "Launch Game")
            {
                string thwargPath = @"C:\Program Files (x86)\Thwargle Games\ThwargLauncher\ThwargLauncher.exe";
                if (!File.Exists(thwargPath))
                {
                    thwargPath = Properties.Settings.Default.ThwargLauncherPath;
                }

                if (!File.Exists(thwargPath))
                {
                    var result = MessageBox.Show("ThwargLauncher.exe not found. Would you like to browse for it?", "Launcher Not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var dialog = new Microsoft.Win32.OpenFileDialog
                        {
                            Filter = "Executable Files (*.exe)|*.exe",
                            Title = "Select ThwargLauncher.exe"
                        };
                        if (dialog.ShowDialog() == true)
                        {
                            thwargPath = dialog.FileName;
                            Properties.Settings.Default.ThwargLauncherPath = thwargPath;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = thwargPath,
                        UseShellExecute = true,
                        WorkingDirectory = Path.GetDirectoryName(thwargPath)
                    };
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to start ThwargLauncher:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            InstallButton.IsEnabled = false;
            StatusTextBlock.Text = "Starting installation...";

            var itemsToInstall = _installItems.Where(item => item.IsSelected && item.Status != InstallStatus.Installed).ToList();

            for (int i = 0; i < itemsToInstall.Count; i++)
            {
                var item = itemsToInstall[i];
                item.Status = InstallStatus.Installing;
                StatusTextBlock.Text = $"Installing {item.Name}...";

                bool success = await InstallComponent(item.Name);

                if (success)
                {
                    item.Status = InstallStatus.Installed;
                }
                else
                {
                    item.Status = InstallStatus.Failed;
                    StatusTextBlock.Text = $"Failed to install {item.Name}.";
                    InstallButton.IsEnabled = true;
                    SaveInstallState();
                    return;
                }
                OverallProgressBar.Value = (double)_installItems.Count(it => it.Status == InstallStatus.Installed) / _installItems.Count * 100;
                SaveInstallState();
            }

            StatusTextBlock.Text = "Installation Complete!";
            InstallButton.Content = "Launch Game";
            InstallButton.IsEnabled = true;
            if (File.Exists(_saveStateFile))
            {
                File.Delete(_saveStateFile);
            }
        }
        
        private async Task<bool> InstallComponent(string? componentName)
        {
            if (string.IsNullOrEmpty(componentName)) return false;
            
            try
            {
                string installerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                switch (componentName)
                {
                    case "Asheron's Call Client":
                        return await RunProcessAsync(Path.Combine(installerPath, "Asherons Call Installer", "ac1install.exe"));
                    case "Client Update":
                        return await Task.Run(() => CopyClientUpdate(installerPath));
                    case "Visual Studio Runtimes":
                        return await RunProcessAsync(Path.Combine(installerPath, "Visual Studio All in One", "install_all.bat"));
                    case ".NET Frameworks":
                        await RunProcessAsync(Path.Combine(installerPath, "DotNet Framework", "dotnetfx35.exe"));
                        return await RunProcessAsync(Path.Combine(installerPath, "DotNet Framework", "windowsdesktop-runtime-9.0.7-win-x64.exe"));
                    case "Decal":
                        return await RunProcessAsync(Path.Combine(installerPath, "Decal", "Decal.msi"));
                    case "Thwarg Launcher":
                        return await RunProcessAsync(Path.Combine(installerPath, "Thwarg Launcher", "ThwargLauncherInstaller.exe"));
                    case "Virindi Tank":
                        return await RunProcessAsync(Path.Combine(installerPath, "Virindi Tank Install", "VirindiInstaller.exe"));
                    case "MagTools":
                        return await Task.Run(() => InstallMagTools(installerPath));
                    case "Utility Belt":
                        return await RunProcessAsync(Path.Combine(installerPath, "Utility Belt Install", "UtilityBeltInstaller-0.2.7.exe"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while installing {componentName}: {ex.Message}", "Installation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return false;
        }

        private async Task<bool> RunProcessAsync(string fileName, string arguments = "")
        {
            try
            {
                ProcessStartInfo startInfo;
                if (Path.GetExtension(fileName).Equals(".msi", StringComparison.OrdinalIgnoreCase))
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = "msiexec.exe",
                        Arguments = $"/i \"{fileName}\" /quiet /norestart",
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                }
                else
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        UseShellExecute = true,
                        Verb = "runas" // Request administrator privileges
                    };
                }

                var process = new Process { StartInfo = startInfo };
                process.Start();
                await process.WaitForExitAsync();
                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to run installer: {fileName}\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool CopyClientUpdate(string basePath)
        {
            string sourceDir = Path.Combine(basePath, "Asherons Call Update");
            string destDir = @"C:\Turbine\Asheron's Call";

            if (!Directory.Exists(destDir))
            {
                var result = MessageBox.Show("Asheron's Call installation directory not found at C:\\Turbine\\Asheron's Call. Would you like to browse for the folder?", "Folder Not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var dialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        destDir = dialog.SelectedPath;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), true);
            }
            return true;
        }

        private bool InstallMagTools(string basePath)
        {
            string destDir = @"C:\Games\Decal Plugins\Mag Tools";
            Directory.CreateDirectory(destDir);
            string sourceFile = Path.Combine(basePath, "Magtools", "MagTools.dll");
            string destFile = Path.Combine(destDir, "MagTools.dll");
            File.Copy(sourceFile, destFile, true);

            MessageBox.Show("MagTools has been copied. Please open Decal, click 'Add', navigate to 'C:\\Games\\Decal Plugins\\Mag Tools', select 'MagTools.dll' and click 'Save'.\n\nIf it fails, right-click the DLL in that directory and click 'Unblock'.", "Manual Step Required", MessageBoxButton.OK, MessageBoxImage.Information);

            return true;
        }
    }

    public class InstallItem : INotifyPropertyChanged
    {
        private string? _name;
        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private InstallStatus _status;
        public InstallStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum InstallStatus
    {
        NotStarted,
        Installing,
        Installed,
        Failed
    }

    public class StatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((InstallStatus)value)
            {
                case InstallStatus.NotStarted:
                    return "❓";
                case InstallStatus.Installing:
                    return "⏳";
                case InstallStatus.Installed:
                    return "✔️";
                case InstallStatus.Failed:
                    return "❌";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
