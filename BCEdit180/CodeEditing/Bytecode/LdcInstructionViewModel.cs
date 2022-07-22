using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class LdcInstructionViewModel : BaseInstructionViewModel {
        private object value;
        public object Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public override IEnumerable<Opcode> AvailableOpcodes => new[] { Opcode.LDC};

        public LdcInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            this.Value = insn.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            insn.Value = this.Value;
        }
    }
}