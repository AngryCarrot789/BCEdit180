using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class TableSwitchInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.TABLESWITCH};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            TableSwitchInstruction insn = (TableSwitchInstruction) instruction;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            TableSwitchInstruction insn = (TableSwitchInstruction) instruction;
        }
    }
}