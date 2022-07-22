using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class MethodInstructionViewModel : BaseInstructionViewModel {
        private string ownerClass;
        public string OwnerClass {
            get => this.ownerClass;
            set => RaisePropertyChanged(ref this.ownerClass, value);
        }

        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private string descriptor;
        public string Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public MethodInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            this.OwnerClass = insn.Owner.Name;
            this.MethodName = insn.Name;
            this.Descriptor = insn.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            insn.Owner = new ClassName(this.OwnerClass);
            insn.Name = this.MethodName;
            insn.Descriptor = MethodDescriptor.Parse(this.Descriptor);
        }
    }
}