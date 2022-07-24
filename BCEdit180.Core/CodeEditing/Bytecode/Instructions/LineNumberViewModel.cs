using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LineNumberViewModel : BaseInstructionViewModel {
        private ushort lineNumber;
        public ushort LineNumber {
            get => this.lineNumber;
            set => RaisePropertyChanged(ref this.lineNumber, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.None};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LineNumber insn = (LineNumber) instruction;
            this.LineNumber = insn.Line;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LineNumber insn = (LineNumber) instruction;
            insn.Line = this.LineNumber;
        }
    }
}