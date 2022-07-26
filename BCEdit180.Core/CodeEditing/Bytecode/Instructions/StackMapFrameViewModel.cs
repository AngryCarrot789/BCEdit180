using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using BCEdit180.Core.Commands;
using BCEdit180.Core.Window;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class StackMapFrameViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override bool CanEditOpCode => false;

        private FrameType frameType;
        public FrameType FrameType {
            get => this.frameType;
            set => RaisePropertyChanged(ref this.frameType, value);
        }

        private int stack;
        public int Stack {
            get => this.stack;
            set => RaisePropertyChanged(ref this.stack, value);
        }

        private int locals;
        public int Locals {
            get => this.locals;
            set => RaisePropertyChanged(ref this.locals, value);
        }

        public ICommand EditFrameTypeCommand { get; }

        public StackMapFrameViewModel() {
            this.EditFrameTypeCommand = new ExtendedRelayCommand(EditFrameTypeAction, () => false);
        }

        public void EditFrameTypeAction() {
            if (Dialog.TypeEditor.EditEnumFlagDialog(this.FrameType, out FrameType frameType).Result) {
                this.FrameType = frameType;
            }
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
            this.FrameType = insn.Type;
            this.Stack = insn.Stack.Count;
            this.Locals = insn.Locals.Count;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            StackMapFrame insn = (StackMapFrame) instruction;
            insn.Type = this.FrameType;
        }
    }
}