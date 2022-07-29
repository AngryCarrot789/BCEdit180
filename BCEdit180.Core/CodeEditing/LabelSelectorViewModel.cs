using BCEdit180.Core.CodeEditing.Bytecode;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing {
    public class JumpTargetSelectorViewModel : BaseViewModel, IBytecodeEditorAccess {
        public BytecodeEditorViewModel BytecodeEditor { get; set; }

        private LabelViewModel target;
        public LabelViewModel Target {
            get => this.target;
            set => RaisePropertyChanged(ref this.target, value);
        }

        private BaseInstructionViewModel selectedInstruction;
        public BaseInstructionViewModel SelectedInstruction {
            get => this.selectedInstruction;
            set => RaisePropertyChanged(ref this.selectedInstruction, value);
        }
    }
}