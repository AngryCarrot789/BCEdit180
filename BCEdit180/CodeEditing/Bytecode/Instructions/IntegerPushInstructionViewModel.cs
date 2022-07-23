using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class IntegerPushInstructionViewModel : BaseInstructionViewModel {
        private ushort value;
        public ushort Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.BIPUSH, Opcode.SIPUSH};

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