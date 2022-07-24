using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class LookupSwitchInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LOOKUPSWITCH};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;

        }

        public override void Save(Instruction instruction) {
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;
            base.Save(instruction);
        }
    }
}