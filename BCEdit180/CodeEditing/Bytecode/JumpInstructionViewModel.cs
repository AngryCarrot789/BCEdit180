using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class JumpInstructionViewModel : BaseInstructionViewModel {
        private long target;
        public long Target {
            get => this.target;
            set => RaisePropertyChanged(ref this.target, value);
        }

        public JumpInstructionViewModel(Instruction instruction) : base(instruction) {

        }

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