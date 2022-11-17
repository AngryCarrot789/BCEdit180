using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BCEdit180.Core.Dialog;
using REghZy.MVVM.Commands;

namespace BCEdit180.Windows.Base {
    public class DialogBase : WindowBase {
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public DialogBase() {
            this.ConfirmCommand = new RelayCommand(OnUserConfirmDialog);
            this.CancelCommand = new RelayCommand(OnUserCancelDialog);
        }

        public virtual void OnUserConfirmDialog() {
            this.DialogResult = true;
            OnCloseDialogGeneral();
        }

        public virtual void OnUserCancelDialog() {
            this.DialogResult = false;
            OnCloseDialogGeneral();
        }

        public virtual void OnCloseDialogGeneral() {
            Close();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Escape) {
                OnUserCancelDialog();
            }
            else if (e.Key == Key.Enter) {
                OnUserConfirmDialog();
            }
        }
        
        public Task<bool> ShowDialogAsync<T>(T model) where T : BaseDialogViewModel {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Application.Current.Dispatcher.Invoke(() => {
                try {
                    this.Owner = Application.Current.MainWindow;
                    this.DataContext = model;
                    ShowDialog();
                }
                finally {
                    // Let caller know we finished
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}