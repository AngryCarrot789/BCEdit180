using JavaAsm.Instructions;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode {
    // non-abstract, so that opcodes can fall back to the base class and get ToString()'d instead of custom layouts
    public class BaseInstructionViewModel : BaseViewModel {
        public Instruction Instruction { get; protected set; }

        private Opcode opCode;
        public Opcode Opcode {
            get => this.opCode;
            set => RaisePropertyChanged(ref this.opCode, value);
        }

        public BaseInstructionViewModel(Instruction instruction) {
            this.Instruction = instruction;
        }

        public virtual void Load(Instruction instruction) {
            this.Opcode = instruction.Opcode;
        }

        public virtual void Save(Instruction instruction) {
            instruction.Opcode = this.Opcode;
        }
    }
}
