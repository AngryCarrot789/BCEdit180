using System.Windows;

namespace BCEdit180.Dialogs {
    public static class Dialog {
        public static void ShowDialog(string title, string message) {
            MessageBox.Show(message, title);
        }

        public static DialogMessageViewModel ShowMessage(string header, bool isLoading = true) {
            DialogMessageWindow window = new DialogMessageWindow();
            DialogMessageViewModel vm = new DialogMessageViewModel(()=> {
                // Window might not be thread-safe. Haven't tested yet though
                Application.Current.Dispatcher.Invoke(window.Close);
            });

            vm.HeaderMessage = header;
            vm.Description = "Loading...";
            vm.IsLoading = isLoading;
            window.DataContext = vm;
            window.Show();
            return vm;
        }
    }
}
