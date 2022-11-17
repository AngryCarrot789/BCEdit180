using System.Collections.Generic;
using System.Windows.Input;
using BCEdit180.Core.Commands;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class JumpInstructionViewModel : BaseInstructionViewModel, IBytecodeEditorAccess, ILabelTargeter {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IFEQ, Opcode.IFNE, Opcode.IFLT, Opcode.IFGE, Opcode.IFGT, Opcode.IFLE, Opcode.IF_ICMPEQ, Opcode.IF_ICMPNE, Opcode.IF_ICMPLT, Opcode.IF_ICMPGE, Opcode.IF_ICMPGT, Opcode.IF_ICMPLE, Opcode.IF_ACMPEQ, Opcode.IF_ACMPNE, Opcode.GOTO, Opcode.JSR, Opcode.IFNULL, Opcode.IFNONNULL};

        private long labelIndex;
        public long LabelIndex {
            get => this.labelIndex;
            set => RaisePropertyChanged(ref this.labelIndex, value);
        }

        public ICommand SelectJumpDestinationCommand { get; }

        private BytecodeEditorViewModel bytecodeEditor;
        public BytecodeEditorViewModel BytecodeEditor {
            get => this.bytecodeEditor;
            set {
                this.bytecodeEditor = value;
                this.EditTargetLabelCommand.RaiseCanExecuteChanged();
            }
        }

        private LabelViewModel targetLabel;
        public LabelViewModel TargetLabel {
            get => this.targetLabel;
            set => RaisePropertyChanged(ref this.targetLabel, value);
        }

        private int jumpOffset;
        public int JumpOffset {
            get => this.jumpOffset;
            set => RaisePropertyChanged(ref this.jumpOffset, value);
        }

        public ExtendedRelayCommand EditTargetLabelCommand { get; }

        public JumpInstructionViewModel() {
            this.SelectJumpDestinationCommand = new RelayCommand(SelectJumpDestinationAction);
            this.EditTargetLabelCommand = new ExtendedRelayCommand(EditTargetLabelAction, () => this.BytecodeEditor != null);
        }

        public void EditTargetLabelAction() {
            if (this.BytecodeEditor != null) {
                this.BytecodeEditor.EditBranchTargetAction(this);
            }
        }

        public void SelectJumpDestinationAction() {
            if (this.BytecodeEditor != null && this.TargetLabel != null) {
                this.BytecodeEditor.SelectedInstruction = this.TargetLabel;
                BytecodeEditorViewModel.BytecodeList.ScrollToSelectedItem();
            }
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            JumpInstruction jump = (JumpInstruction) instruction;
            if (jump.Target != null) {
                this.LabelIndex = jump.Target.Index;
            }
            else {
                this.LabelIndex = -1;
            }

            this.JumpOffset = jump.JumpOffset;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            JumpInstruction jump = (JumpInstruction) instruction;
            if (this.TargetLabel != null && this.TargetLabel.Instruction != null) {
                jump.Target = this.TargetLabel.Label;
            }
        }
    }
}