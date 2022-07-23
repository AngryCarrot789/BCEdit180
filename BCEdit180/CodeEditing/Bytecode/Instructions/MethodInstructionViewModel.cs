using System.Collections.Generic;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class MethodInstructionViewModel : BaseInstructionViewModel {
        private string methodOwner;
        public string MethodOwner {
            get => this.methodOwner;
            set => RaisePropertyChanged(ref this.methodOwner, value);
        }

        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private string methodDescriptor;
        public string MethodDescriptor {
            get => this.methodDescriptor;
            set => RaisePropertyChanged(ref this.methodDescriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.INVOKESTATIC, Opcode.INVOKEVIRTUAL, Opcode.INVOKEINTERFACE, Opcode.INVOKESPECIAL};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            this.MethodOwner = insn.Owner.Name;
            this.MethodName = insn.Name;
            this.MethodDescriptor = insn.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            MethodInstruction insn = (MethodInstruction) instruction;
            insn.Owner = new ClassName(this.MethodOwner);
            insn.Name = this.MethodName;
            insn.Descriptor = JavaAsm.MethodDescriptor.Parse(this.MethodDescriptor);
        }
    }
}