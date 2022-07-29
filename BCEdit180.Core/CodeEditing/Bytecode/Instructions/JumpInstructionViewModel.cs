using System;
using System.Collections.Generic;
using System.Windows.Input;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class JumpInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.IFEQ, Opcode.IFNE, Opcode.IFLT, Opcode.IFGE, Opcode.IFGT, Opcode.IFLE, Opcode.IF_ICMPEQ, Opcode.IF_ICMPNE, Opcode.IF_ICMPLT, Opcode.IF_ICMPGE, Opcode.IF_ICMPGT, Opcode.IF_ICMPLE, Opcode.IF_ACMPEQ, Opcode.IF_ACMPNE, Opcode.GOTO, Opcode.JSR, Opcode.IFNULL, Opcode.IFNONNULL};

        private long target;
        public long Target {
            get => this.target;
            set => RaisePropertyChanged(ref this.target, value);
        }

        public ICommand SelectJumpDestinationCommand { get; }

        public BytecodeEditorViewModel BytecodeEditor { get; set; }

        public LabelViewModel JumpDestination { get; set; }

        private int originalJumpOffset;
        private int jumpOffset;
        public int JumpOffset {
            get => this.jumpOffset;
            set => RaisePropertyChanged(ref this.jumpOffset, value);
        }

        public JumpInstructionViewModel() {
            this.SelectJumpDestinationCommand = new RelayCommand(SelectJumpDestinationAction);
        }

        public void SelectJumpDestinationAction() {
            if (this.BytecodeEditor != null && this.JumpDestination != null) {
                this.BytecodeEditor.SelectedInstruction = this.JumpDestination;
                BytecodeEditorViewModel.BytecodeList.ScrollToSelectedItem();
            }
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            JumpInstruction jump = (JumpInstruction) instruction;
            this.Target = jump.Target.Index;
            this.JumpOffset = jump.JumpOffset;
            this.originalJumpOffset = jump.JumpOffset;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            JumpInstruction jump = (JumpInstruction) instruction;
            // jump.Target = new Label();
            if (this.originalJumpOffset != this.JumpOffset) {
                jump.JumpOffset = this.JumpOffset;
                jump.UseOverrideOffset = true;
            }
        }
    }
}