using BCEdit180.Core.Editors;
using BCEdit180.Core.Modals;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for FlagEditorWindow.xaml
    /// </summary>
    public partial class FlagEditorWindow : WindowModal, IModalWindow<FlagEditorViewModel> {
        public FlagEditorViewModel Model {
            get => (FlagEditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public FlagEditorWindow() {
            InitializeComponent();
        }
    }
}
