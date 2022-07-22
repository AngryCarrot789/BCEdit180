using System.Windows;
using System.Windows.Input;

namespace BCEdit180.CodeEditing.InstructionEdit {
    /// <summary>
    /// Interaction logic for ChangeInstructionWindow.xaml
    /// </summary>
    public partial class ChangeInstructionWindow : Window {
        public ChangeInstructionWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            if (e.Key == Key.Enter) {
                ((ChangeInstructionViewModel) this.DataContext).ApplyChanges();
                Close();
            }
            else if (e.Key == Key.Escape) {
                Close();
            }
        }

        // easy solution
        private void OkayClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
