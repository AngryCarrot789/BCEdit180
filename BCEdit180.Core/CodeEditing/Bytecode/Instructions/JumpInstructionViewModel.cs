using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class JumpInstructionViewModel : BaseInstructionViewModel {
        private long target;
        public long Target {
            get => this.target;
            set => RaisePropertyChanged(ref this.target, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IFEQ, Opcode.IFNE, Opcode.IFLT, Opcode.IFGE, Opcode.IFGT, Opcode.IFLE, Opcode.IF_ICMPEQ, Opcode.IF_ICMPNE, Opcode.IF_ICMPLT, Opcode.IF_ICMPGE, Opcode.IF_ICMPGT, Opcode.IF_ICMPLE, Opcode.IF_ACMPEQ, Opcode.IF_ACMPNE, Opcode.GOTO, Opcode.JSR, Opcode.IFNULL, Opcode.IFNONNULL};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            JumpInstruction insn = (JumpInstruction) instruction;
            this.Target = insn.Target.Index;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            // JumpInstruction insn = (JumpInstruction) instruction;
            // insn.Target = new Label();
            // insn.Descriptor = MethodDescriptor.Parse(this.Descriptor);
        }
    }
}