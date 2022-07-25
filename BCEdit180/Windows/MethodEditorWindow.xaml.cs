using BCEdit180.Core.Editors;
using BCEdit180.Core.Modals;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for MethodCreatorWindow.xaml
    /// </summary>
    public partial class MethodEditorWindow : WindowModal, IModalWindow<MethodEditorViewModel> {
        public MethodEditorViewModel Model {
            get => (MethodEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public MethodEditorWindow() {
            InitializeComponent();
        }
    }
}
