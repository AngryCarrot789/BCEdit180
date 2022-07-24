using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
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

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IINC};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            this.VarIndex = increment.VariableIndex;
            this.Value = increment.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            IncrementInstruction increment = (IncrementInstruction) instruction;
            increment.VariableIndex = this.VarIndex;
            increment.Value = this.Value;
        }
    }
}