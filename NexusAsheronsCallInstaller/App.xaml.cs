using System;
using System.IO;
using System.Windows;

namespace NexusAsheronsCallInstaller
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            string errorMessage = $"An unhandled exception occurred: {e.Exception.Message}\n\n{e.Exception.StackTrace}";
            File.WriteAllText("error.log", errorMessage);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }
}
