using BCEdit180.Core.Editors;
using BCEdit180.Core.Modals;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for TypeEditorViewModel.xaml
    /// </summary>
    public partial class TypeEditorWindow : WindowModal, IModalWindow<TypeEditorViewModel> {
        public TypeEditorViewModel Model {
            get => (TypeEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public TypeEditorWindow() {
            InitializeComponent();
        }
    }
}
