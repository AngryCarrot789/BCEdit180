using BCEdit180.Core.CodeEditing.Bytecode;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Dialog;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing {
    public class LabelSelectorViewModel : BaseDialogViewModel, IBytecodeEditorAccess {
        public BytecodeEditorViewModel BytecodeEditor { get; set; }

        private BaseInstructionViewModel selectedInstruction;
        public BaseInstructionViewModel SelectedInstruction {
            get => this.selectedInstruction;
            set => RaisePropertyChanged(ref this.selectedInstruction, value);
        }
    }
}