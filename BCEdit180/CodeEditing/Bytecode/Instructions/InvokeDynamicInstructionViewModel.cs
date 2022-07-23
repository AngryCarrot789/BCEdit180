using System.Collections.Generic;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class InvokeDynamicInstructionViewModel : BaseInstructionViewModel {
        private string name;
        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        private string descriptor;
        public string Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.INVOKEDYNAMIC};

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            this.Name = insn.Name;
            this.Descriptor = insn.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            InvokeDynamicInstruction insn = (InvokeDynamicInstruction) instruction;
            insn.Name = this.Name;
            insn.Descriptor = MethodDescriptor.Parse(this.Descriptor);
        }
    }
}