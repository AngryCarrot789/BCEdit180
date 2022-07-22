using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class IntegerPushInstructionViewModel : BaseInstructionViewModel {
        private ushort value;
        public ushort Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public IntegerPushInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            IntegerPushInstruction insn = (IntegerPushInstruction) instruction;
            this.Value = insn.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            IntegerPushInstruction insn = (IntegerPushInstruction) instruction;
            insn.Value = (ushort) this.Value;
        }
    }
}