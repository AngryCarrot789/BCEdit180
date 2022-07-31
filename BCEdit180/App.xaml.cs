using System;
using System.Windows;
using BCEdit180.Core.ViewModels;
using System.IO;

namespace BCEdit180 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow window = new MainWindow();
            ClassListViewModel classes = new ClassListViewModel();
            window.SetupServices();
            if (e.Args.Length > 0) {
                string path = string.Join(" ", e.Args);
                if (File.Exists(path)) {
                    classes.OpenAndReadClassFile(path, true);
                }
                else {
                    MessageBox.Show("File does not exist: " + path, "File not found");
                }
            }

            if (classes.Classes.Count < 1) {
                classes.CreateBalnkClass();
            }

            classes.SelectedClass = classes.Classes[0];

            window.LoadXML();
            this.MainWindow = window;
            this.MainWindow.DataContext = classes;
            window.Show();
        }
    }
}
