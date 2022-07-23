using System.Windows;
using System.Windows.Input;
using BCEdit180.CodeEditing.InstructionEdit;

namespace BCEdit180.TypeEditor {
    /// <summary>
    /// Interaction logic for TypeEditorViewModel.xaml
    /// </summary>
    public partial class TypeEditorWindow : Window {
        public TypeEditorWindow() {
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
            ((ChangeInstructionViewModel) this.DataContext).ApplyChanges();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
