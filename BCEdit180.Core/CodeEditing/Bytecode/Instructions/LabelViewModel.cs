using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LabelViewModel : BaseInstructionViewModel, IBytecodeEditorAccess {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override bool CanEditOpCode => false;

        public BytecodeEditorViewModel BytecodeEditor { get; set; }

        private long index;
        public long Index {
            get => this.index;
            set {
                RaisePropertyChanged(ref this.index, value);
                this.BytecodeEditor.VerifyDuplicateLabelIndex(this);
            }
        }

        public Label Label => (Label) base.Instruction;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            Label label = (Label) instruction;
            this.Index = label.Index;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
        }
    }
}