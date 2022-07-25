using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Window;
using JavaAsm.CustomAttributes;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LdcInstructionViewModel : BaseInstructionViewModel {
        private object value;
        public object Value {
            get => this.value;
            set {
                RaisePropertyChanged(ref this.value, value);
                this.IsEditable = value is string;
            }
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LDC};

        public override bool CanEditOpCode => false;

        private bool isEditable;
        public bool IsEditable {
            get => this.isEditable;
            set => RaisePropertyChangedCheckEqual(ref this.isEditable, value);
        }

        public ICommand EditValueCommand { get; }

        public LdcInstructionViewModel() {
            this.EditValueCommand = new RelayCommand(EditValueAction);
        }

        public void EditValueAction() {
            ConstValueEditorViewModel editor = Dialog.TypeEditor.EditConstantDialog(new ConstValueEditorViewModel(this.Value)).Result;
            if (editor != null) {
                this.Value = editor.GetValue();
            }
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            this.Value = insn.Value;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LdcInstruction insn = (LdcInstruction) instruction;
            insn.Value = this.Value;
        }
    }
}