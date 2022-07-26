using System.Collections.Generic;
using System.Windows.Input;
using BCEdit180.Core.Commands;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
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

        private TypeDescriptor fieldDescriptor;
        public TypeDescriptor FieldDescriptor {
            get => this.fieldDescriptor;
            set => RaisePropertyChanged(ref this.fieldDescriptor, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.GETFIELD, Opcode.GETSTATIC, Opcode.PUTFIELD, Opcode.PUTSTATIC};

        public ICommand EditDescriptorCommand { get; }

        public FieldInstructionViewModel() {
            this.EditDescriptorCommand = new RelayCommand(EditDescriptorAction);
        }

        public void EditDescriptorAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.FieldDescriptor, out TypeDescriptor descriptor).Result) {
                this.FieldDescriptor = descriptor;
            }
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            this.FieldOwner = field.Owner.Name;
            this.FieldName = field.Name;
            this.FieldDescriptor = field.Descriptor;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            FieldInstruction field = (FieldInstruction) instruction;
            field.Descriptor = this.FieldDescriptor;
            field.Owner = new ClassName(this.FieldOwner);
            field.Name = this.FieldName;
        }
    }
}