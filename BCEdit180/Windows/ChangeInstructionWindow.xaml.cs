using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Modals;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for ChangeInstructionWindow.xaml
    /// </summary>
    public partial class ChangeInstructionWindow : WindowModal, IModalWindow<ChangeInstructionViewModel> {
        public ChangeInstructionViewModel Model {
            get => (ChangeInstructionViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public ChangeInstructionWindow() {
            InitializeComponent();
        }
    }
}
