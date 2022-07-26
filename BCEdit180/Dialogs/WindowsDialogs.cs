using System.Threading.Tasks;
using System.Windows;
using BCEdit180.Core.Window;
using BCEdit180.Windows;

namespace BCEdit180.Dialogs {
    public class WindowsDialogs : IDialogManager {
        public Task ShowInformationDialog(string header, string description) {
            // ErrorDialogWindow window = new ErrorDialogWindow();
            // window.Title = header;
            // window.DataContext = new ErrorDialogViewModel() {
            //     Description = description
            // };
            // window.ShowDialog();
            MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Information);
            return Task.CompletedTask;
        }

        public Task ShowWarningDialog(string header, string description) {
            ErrorDialogWindow window = new ErrorDialogWindow();
            window.Title = $"Warning: {header}";
            window.DataContext = new ErrorDialogViewModel() {
                Description = description
            };

            window.ShowDialog();

            // MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Warning);
            return Task.CompletedTask;
        }

        public Task<bool> ConfirmOkCancel(string header, string description) {
            return ConfirmOkCancel(header, description, true);
        }

        public async Task<bool> ConfirmOkCancel(string header, string description, bool defaultResult) {
            return MessageBoxResult.OK == MessageBox.Show(description, header, MessageBoxButton.OKCancel, MessageBoxImage.Question, defaultResult ? MessageBoxResult.OK : MessageBoxResult.Cancel);
        }

        public ActionProgressViewModel ShowProgressWindow(string header, string description = null) {
            ActionProgressWindow window = new ActionProgressWindow();
            ActionProgressViewModel vm = new ActionProgressViewModel(()=> {
                // Window might not be thread-safe. Haven't tested yet though
                Application.Current.Dispatcher.Invoke(window.Close);
            });

            vm.HeaderMessage = header;
            vm.Description = description;
            vm.IsLoading = true;
            window.DataContext = vm;
            window.Show();
            return vm;
        }
    }
}
