using System.Windows;
using System.Windows.Input;
using BCEdit180.Core.CodeEditing.InstructionEdit;

namespace BCEdit180.Windows {
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
        private void Okay_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
