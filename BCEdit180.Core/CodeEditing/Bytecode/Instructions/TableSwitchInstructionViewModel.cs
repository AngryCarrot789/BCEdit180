using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class TableSwitchInstructionViewModel : BaseSwitchInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.TABLESWITCH};

        public override bool CanEditOpCode => false;

        private int lowValue;
        public int LowValue {
            get => this.lowValue;
            set => RaisePropertyChanged(ref this.lowValue, value);
        }

        private int highValue;
        public int HighValue {
            get => this.highValue;
            set => RaisePropertyChanged(ref this.highValue, value);
        }


        public TableSwitchInstructionViewModel() : base() {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            TableSwitchInstruction switchTable = (TableSwitchInstruction) instruction;
            this.DefaultLabel = new MatchLabelViewModel() {IsDefault = true};
            SetCallbacks(this.DefaultLabel);
            this.DefaultLabel.Load(-2, switchTable.Default);
            this.DefaultIndex = switchTable.Default?.Index ?? -1;
            this.MatchLabels.Clear();
            this.LowValue = switchTable.LowValue;
            this.HighValue = switchTable.HighValue;
            int index = 0;
            foreach (Label label in switchTable.Labels) {
                MatchLabelViewModel match = new MatchLabelViewModel();
                SetCallbacks(match);
                match.Load(index, label);
                this.MatchLabels.Add(match);
                index++;
            }
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            TableSwitchInstruction switchTable = (TableSwitchInstruction) instruction;
            switchTable.LowValue = this.LowValue;
            switchTable.HighValue = this.HighValue;
            switchTable.Labels.Clear();
            switchTable.Default = this.DefaultLabel?.Label;
            switchTable.Labels.AddRange(this.MatchLabels.OrderBy(t => t.SwitchIndex).Select(t => t.Label));
        }
    }
}