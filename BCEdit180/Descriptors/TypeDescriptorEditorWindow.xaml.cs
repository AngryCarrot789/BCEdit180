using System.Windows;
using System.Windows.Input;

namespace BCEdit180.Descriptors {
    public partial class TypeDescriptorEditorWindow : Window {
        public TypeDescriptorEditorWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            if (e.Key == Key.Enter) {
                ((TypeDescriptorEditorViewModel) this.DataContext).ApplyChange();
                Close();
            }
            else if (e.Key == Key.Escape) {
                Close();
            }
        }

        private void OkayClick(object sender, RoutedEventArgs e) {
            ((TypeDescriptorEditorViewModel) this.DataContext).ApplyChange();
            this.Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}