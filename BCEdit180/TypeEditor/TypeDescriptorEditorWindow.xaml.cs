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
using JavaAsm;
using REghZy.MVVM.Views;

namespace BCEdit180.TypeEditor {
    /// <summary>
    /// Interaction logic for TypeDescriptorEditorWindow.xaml
    /// </summary>
    public partial class TypeDescriptorEditorWindow : Window, BaseView<DescriptorEditorViewModel> {
        public Action<TypeDescriptor> ApplyChanges { get; }

        public DescriptorEditorViewModel Model {
            get => (DescriptorEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public TypeDescriptorEditorWindow(Action<TypeDescriptor> applyChanges) {
            InitializeComponent();
            this.ApplyChanges = applyChanges;
        }

        private void Okay_Click(object sender, RoutedEventArgs e) {
            this.ApplyChanges(this.Model.Descriptor);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {

        }
    }
}
