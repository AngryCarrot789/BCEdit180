using System.Windows;
using System.Windows.Input;
using BCEdit180.Core.Editors;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for FlagEditorWindow.xaml
    /// </summary>
    public partial class FlagEditorWindow : Window {
        public FlagEditorViewModel Model {
            get => (FlagEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public FlagEditorWindow() {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);

            if (e.Key == Key.Enter) {
                ApplyChange();
                Close();
            }
            else if (e.Key == Key.Escape) {
                Close();
            }
        }

        public void ApplyChange() {
            this.Model.ApplyChange();
        }

        // too lazy to use command bindings
        private void Okay_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
