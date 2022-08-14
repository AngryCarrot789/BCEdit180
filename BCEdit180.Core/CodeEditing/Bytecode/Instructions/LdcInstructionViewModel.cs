using System.Collections.Generic;
using System.Windows.Input;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Window;
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
                if (this.Opcode != Opcode.LDC2_W && (value is long || value is double)) {
                    this.Opcode = Opcode.LDC2_W;
                }
            }
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LDC, Opcode.LDC_W, Opcode.LDC2_W};

        public override bool CanEditOpCode => true;

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
            if (Dialog.TypeEditor.EditConstantDialog(new ConstValueEditorViewModel(this.Value), out ConstValueEditorViewModel editor).Result) {
                if (editor.CheckEnabledStatesWithDialog()) {
                    if (editor.TryGetValue(out object value, out string error)) {
                        this.Value = value;
                    }
                    else if (error != null) {
                        Dialog.Message.ShowInformationDialog("Invalid value", error);
                    }
                }
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