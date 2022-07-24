using System.Windows;
using System.Windows.Input;
using BCEdit180.Core.Editors;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for FieldEditorWindow.xaml
    /// </summary>
    public partial class FieldEditorWindow : Window {
        public FieldEditorWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter) {
                ((FieldEditorViewModel) this.DataContext).ApplyChanges();
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
