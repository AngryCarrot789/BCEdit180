using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.Utils;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LabelViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override bool CanEditOpCode => false;

        private long index;
        public long Index {
            get => this.index;
            set => RaisePropertyChanged(ref this.index, value);
        }

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