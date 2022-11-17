using System;
using System.Windows;
using BCEdit180.Core.ViewModels;
using System.IO;
using BCEdit180.Core.Commands;
using BCEdit180.Core.Window;
using BCEdit180.Core;
using BCEdit180.Dialog;
using BCEdit180.Dialogs;

namespace BCEdit180 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            ServiceManager.SetService<IDialogManager>(new WindowsDialogs());
            ServiceManager.SetService<ITypeEditors>(new WindowsTypeEditors());
            ServiceManager.SetService<IFileDialog>(new WindowsFileDialogs());
            ServiceManager.SetService<IApplicationProxy>(new WpfApplicationProxy());
            ServiceManager.SetService<ICommandManager>(new WPFCommandManager());
            ServiceManager.SetService<IUIManager>(new UIManager());

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

        // RoundNumber(95.12345, 3) -> 95.123
        public static double RoundNumber(double value, int places) {
            // // 1000.0
            // double placePower = ROUND_POW_10[places];
            // // 95
            // int intValue = (int) value;
            // // 0.12345
            // double decimalPart = value - intValue;
            // // 123.45
            // double decimalPartScaled = placePower * decimalPart;
            // // 123
            // int intDecimalPartScaled = (int) decimalPartScaled;
            // // 123.0 / 1000.0 = 0.123
            // double decimalPartStripped = ((double) intDecimalPartScaled) / placePower;
            // return (double) intValue + decimalPartStripped;

            double placePower = POW_TABLE[places];
            int intValue = (int) value;
            return intValue + (int) (placePower * (value - intValue)) / placePower;
        }

        private static readonly double[] POW_TABLE = new double[] {
            1.0,
            10.0,
            100.0,
            1000.0,
            10000.0,
            100000.0,
            1000000.0,
            10000000.0,
            100000000.0,
            1000000000.0,
            10000000000.0,
            100000000000.0,
            1000000000000.0,
            10000000000000.0,
            100000000000000.0,
            1E+15
        };
    }
}
