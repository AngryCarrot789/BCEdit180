using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Modals;
using BCEdit180.Windows.Base;

namespace BCEdit180.Windows {
    /// <summary>
    /// Interaction logic for ChangeInstructionWindow.xaml
    /// </summary>
    public partial class ChangeInstructionWindow : DialogBase, IModalWindow<ChangeInstructionViewModel> {
        public ChangeInstructionViewModel Model {
            get => (ChangeInstructionViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public ChangeInstructionWindow() {
            InitializeComponent();
        }
    }
}
