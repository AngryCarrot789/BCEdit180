using System.Collections.Generic;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.CodeEditing.Bytecode {
    public class FieldInstructionViewModel : BaseInstructionViewModel {
        private string fieldOwner;
        public string FieldOwner {
            get => this.fieldOwner;
            set => RaisePropertyChanged(ref this.fieldOwner, value);
        }

        private string fieldName;
        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        private string fieldDescriptor;
        public string FieldDescriptor {
            get => this.fieldDescriptor;
            set => RaisePropertyChanged(ref this.fieldDescriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpcodes => new[] {Opcode.GETFIELD, Opcode.GETSTATIC, Opcode.PUTFIELD, Opcode.PUTSTATIC};

        public FieldInstructionViewModel(Instruction instruction) : base(instruction) {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            this.FieldOwner = field.Owner.Name;
            this.FieldName = field.Name;
            this.FieldDescriptor = field.Descriptor.ToString();
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            field.Owner = new ClassName(this.FieldOwner);
            field.Name = this.FieldName;
            field.Descriptor = TypeDescriptor.Parse(this.FieldDescriptor, false);
        }
    }
}