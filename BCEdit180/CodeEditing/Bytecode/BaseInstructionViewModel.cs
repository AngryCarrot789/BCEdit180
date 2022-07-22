using JavaAsm.Instructions;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode {
    public class BaseInstructionViewModel : BaseViewModel {
        public Instruction Instruction { get; protected set; }

        private Opcode opCode;
        public Opcode Opcode {
            get => this.opCode;
            set => RaisePropertyChanged(ref this.opCode, value);
        }

        public BaseInstructionViewModel(Instruction instruction) {
            this.Instruction = instruction;
            Load(instruction);
        }

        public virtual void Load(Instruction instruction) {
            this.Opcode = instruction.Opcode;
        }

        public virtual void Save(Instruction instruction) {
            // bruuuh why cant this be edited :'(
            // instruction.Opcode = this.Opcode;
        }
    }
}
