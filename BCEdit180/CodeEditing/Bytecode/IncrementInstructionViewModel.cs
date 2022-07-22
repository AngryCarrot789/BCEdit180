using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class IncrementInstructionViewModel : BaseInstructionViewModel {
        private ushort varIndex;
        public ushort VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        private short value;
        public short Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public IncrementInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            this.VarIndex = increment.VariableIndex;
            this.Value = increment.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            increment.VariableIndex = (ushort) this.VarIndex;
            increment.Value = (short) this.Value;
        }
    }
}