using System.Collections.Generic;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode.Instructions {
    public class MultiANewArrayInstructionViewModel : BaseInstructionViewModel {
        private string componentType;
        public string ComponentType {
            get => this.componentType;
            set => RaisePropertyChanged(ref this.componentType, value);
        }

        private byte dimensions;
        public byte Dimensions {
            get => this.dimensions;
            set => RaisePropertyChanged(ref this.dimensions, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.MULTIANEWARRAY};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            MultiANewArrayInstruction insn = (MultiANewArrayInstruction) instruction;
            this.ComponentType = insn.Type.Name;
            this.Dimensions = insn.Dimensions;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            MultiANewArrayInstruction insn = (MultiANewArrayInstruction) instruction;
            insn.Type = new ClassName(this.ComponentType);
            insn.Dimensions = this.Dimensions;
        }
    }
}