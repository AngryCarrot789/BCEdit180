using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using REghZy.MVVM.Views;

namespace BCEdit180.FlagEditor {
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
            this.Model.InvokeEnumCallback();
        }

        // too lazy to use command bindings
        private void Okay_Click(object sender, RoutedEventArgs e) {
            ApplyChange();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
