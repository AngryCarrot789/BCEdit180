using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class StackMapFrameViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
        }
    }
}