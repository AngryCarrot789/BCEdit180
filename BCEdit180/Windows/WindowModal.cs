using System.Windows;
using System.Windows.Input;
using REghZy.MVVM.Commands;

namespace BCEdit180.Windows {
    public class WindowModal : WindowBase {
        public static readonly DependencyProperty ConfirmCommandProperty =
            DependencyProperty.Register(
                "ConfirmCommand",
                typeof(ICommand),
                typeof(WindowModal),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(
                "CancelCommand",
                typeof(ICommand),
                typeof(WindowModal),
                new PropertyMetadata(null));

        public ICommand ConfirmCommand {
            get => (ICommand) GetValue(ConfirmCommandProperty);
            set => SetValue(ConfirmCommandProperty, value);
        }

        public ICommand CancelCommand {
            get => (ICommand) GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public WindowModal() {
            this.ConfirmCommand = new RelayCommand(AcceptAction);
            this.CancelCommand = new RelayCommand(CancelAction);
        }

        public virtual void AcceptAction() {
            this.DialogResult = true;
        }

        public virtual void CancelAction() {
            this.DialogResult = false;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (e.Key == Key.Escape) {
                CancelAction();
                Close();
            }
            else if (e.Key == Key.Enter) {
                AcceptAction();
                Close();
            }
        }
    }
}