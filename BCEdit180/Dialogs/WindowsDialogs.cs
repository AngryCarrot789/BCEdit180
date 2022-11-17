using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using BCEdit180.Core.Window;
using BCEdit180.Windows;

namespace BCEdit180.Dialogs {
    public class WindowsDialogs : IDialogManager {
        public static void RunTaskOnMainThread(Action action) {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Application.Current.Dispatcher.Invoke(() => {
                try {
                    action();
                }
                finally {
                    tcs.TrySetResult(true);
                }
            });

            tcs.Task.Wait();
        }

        public static T RunTaskOnMainThread<T>(Func<T> action) {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            Application.Current.Dispatcher.Invoke(() => {
                T val = default;
                try {
                    val = action();
                }
                finally {
                    tcs.TrySetResult(val);
                }
            });

            return tcs.Task.Result;
        }

        public void ShowMessage(string header, string description) {
            // ErrorDialogWindow window = new ErrorDialogWindow();
            // window.Title = header;
            // window.DataContext = new ErrorDialogViewModel() {
            //     Description = description
            // };
            // window.ShowDialog();
            RunTaskOnMainThread(() => {
                MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public void ShowWarning(string header, string description) {
            RunTaskOnMainThread(() => {
                ErrorDialogWindow window = new ErrorDialogWindow();
                window.Title = $"Warning: {header}";
                window.DataContext = new ErrorDialogViewModel() {
                    Description = description
                };

                window.ShowDialog();
            });

            // MessageBox.Show(description, header, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool ConfirmOkCancel(string header, string description) {
            return ConfirmOkCancel(header, description, true);
        }

        public bool ConfirmOkCancel(string header, string description, bool defaultResult) {
            return RunTaskOnMainThread(() => MessageBoxResult.OK == MessageBox.Show(description, header, MessageBoxButton.OKCancel, MessageBoxImage.Question, defaultResult ? MessageBoxResult.OK : MessageBoxResult.Cancel));
        }

        public ActionProgressViewModel ShowProgressWindow(string header, string description = null) {
            return RunTaskOnMainThread(() => {
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
            });
        }
    }
}
