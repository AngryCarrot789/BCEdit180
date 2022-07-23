using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class VariableInstructionViewModel : BaseInstructionViewModel {
        private ushort varIndex;
        public ushort VarIndex {
            get => this.varIndex;
            set => RaisePropertyChanged(ref this.varIndex, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.ILOAD, Opcode.LLOAD, Opcode.FLOAD, Opcode.DLOAD, Opcode.ALOAD, Opcode.ISTORE, Opcode.LSTORE, Opcode.FSTORE, Opcode.DSTORE, Opcode.ASTORE, Opcode.RET};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            VariableInstruction insn = (VariableInstruction) instruction;
            this.VarIndex = insn.VariableIndex;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            VariableInstruction insn = (VariableInstruction) instruction;
            insn.VariableIndex = this.VarIndex;
        }
    }
}