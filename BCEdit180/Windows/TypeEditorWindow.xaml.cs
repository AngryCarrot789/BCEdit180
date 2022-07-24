using System.Windows;
using System.Windows.Input;
using BCEdit180.Core.Editors;

namespace BCEdit180.Windows {
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
                ((TypeEditorViewModel) this.DataContext).ApplyChange();
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
