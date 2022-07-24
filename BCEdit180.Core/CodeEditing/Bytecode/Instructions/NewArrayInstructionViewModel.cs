using System.Collections.Generic;
using System.Collections.ObjectModel;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class NewArrayInstructionViewModel : BaseInstructionViewModel {
        private static readonly ObservableCollection<NewArrayTypeCode> CODES;

        static NewArrayInstructionViewModel() {
            CODES = new ObservableCollection<NewArrayTypeCode> {
                NewArrayTypeCode.Boolean,
                NewArrayTypeCode.Character,
                NewArrayTypeCode.Float,
                NewArrayTypeCode.Double,
                NewArrayTypeCode.Byte,
                NewArrayTypeCode.Short,
                NewArrayTypeCode.Integer,
                NewArrayTypeCode.Long
            };
        }

        public ObservableCollection<NewArrayTypeCode> TypeCodes => CODES;

        private NewArrayTypeCode selectedCode;

        public NewArrayTypeCode SelectedCode {
            get => this.selectedCode;
            set => RaisePropertyChanged(ref this.selectedCode, value);
        }

        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.NEWARRAY};

        public override bool CanEditOpCode => false;

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            NewArrayInstruction insn = (NewArrayInstruction) instruction;
            this.SelectedCode = insn.ArrayType;
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            NewArrayInstruction insn = (NewArrayInstruction) instruction;
            switch (this.SelectedCode) {
                case NewArrayTypeCode.Boolean:
                case NewArrayTypeCode.Character:
                case NewArrayTypeCode.Float:
                case NewArrayTypeCode.Double:
                case NewArrayTypeCode.Byte:
                case NewArrayTypeCode.Short:
                case NewArrayTypeCode.Integer:
                case NewArrayTypeCode.Long:
                    insn.ArrayType = this.SelectedCode;
                    break;
            }
        }
    }
}