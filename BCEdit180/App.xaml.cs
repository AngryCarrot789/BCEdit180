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
            ClassViewModel vm = new ClassViewModel();
            window.SetupServices();
            if (e.Args != null && e.Args.Length > 0) {
                string path = string.Join(" ", e.Args);
                if (File.Exists(path)) {
                    vm.ReadClassFileAndShowDialog(path);
                }
                else {
                    MessageBox.Show("File does not exist: " + path, "File not found");
                }
            }

            this.MainWindow = window;
            this.MainWindow.DataContext = vm;
            window.Show();
        }
    }
}
