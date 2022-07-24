using System.Windows;
using BCEdit180.Core.Editors;
using System.Windows.Input;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for MethodDescEditorWindow.xaml
    /// </summary>
    public partial class MethodDescEditorWindow : Window {
        public MethodDescEditorWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter) {
                ((MethodEditorViewModel) this.DataContext).ApplyChanges();
                Close();
            }
            else if (e.Key == Key.Escape) {
                Close();
            }
        }

        private void Okay_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
