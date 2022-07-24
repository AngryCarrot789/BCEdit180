using System.Collections.Generic;
using JavaAsm.Instructions;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LabelViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
        }
    }
}