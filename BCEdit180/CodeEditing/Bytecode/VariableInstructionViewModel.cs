using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class VariableInstructionViewModel : BaseInstructionViewModel {
        private int varIndex;
        public int VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        public VariableInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            this.VarIndex = ((VariableInstruction) instruction).VariableIndex;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
        }
    }
}
