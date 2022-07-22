using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class TypeInstructionViewModel : BaseInstructionViewModel {
        private string type;
        public string Type {
            get => this.type;
            set => RaisePropertyChanged(ref this.type, value);
        }

        public TypeInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            TypeInstruction insn = (TypeInstruction) instruction;
            this.Type = insn.Type.Name;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            TypeInstruction insn = (TypeInstruction) instruction;
            insn.Type = new ClassName(this.Type);
        }
    }
}