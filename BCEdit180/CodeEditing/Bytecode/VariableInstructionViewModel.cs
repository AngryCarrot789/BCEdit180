using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class VariableInstructionViewModel : BaseInstructionViewModel {
        private int varIndex;
        public int VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        public override IEnumerable<Opcode> AvailableOpcodes => new[] { Opcode.ILOAD, Opcode.LLOAD, Opcode.FLOAD, Opcode.DLOAD, Opcode.ALOAD, Opcode.ISTORE, Opcode.LSTORE, Opcode.FSTORE, Opcode.DSTORE, Opcode.ASTORE, Opcode.RET };

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