using System.Collections.Generic;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LookupSwitchInstructionViewModel : BaseSwitchInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LOOKUPSWITCH};

        public override bool CanEditOpCode => false;

        public LookupSwitchInstructionViewModel() : base() {

        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;
            this.DefaultLabel = new MatchLabelViewModel();
            SetCallbacks(this.DefaultLabel);
            this.DefaultLabel.Load(-2, insn.Default);
            this.DefaultIndex = insn.Default?.Index ?? -1;
            this.MatchLabels.Clear();
            foreach (KeyValuePair<int,Label> pair in insn.MatchLabels) {
                MatchLabelViewModel match = new MatchLabelViewModel();
                SetCallbacks(match);
                match.Load(pair.Key, pair.Value);
                this.MatchLabels.Add(match);
            }
        }

        public override void Save(Instruction instruction) {
            base.Save(instruction);
            LookupSwitchInstruction switchTable = (LookupSwitchInstruction) instruction;
            switchTable.MatchLabels.Clear();
            switchTable.Default = this.DefaultLabel?.Label;
            foreach (MatchLabelViewModel matchLabel in this.MatchLabels) {
                switchTable.MatchLabels.Add(new KeyValuePair<int, Label>(matchLabel.SwitchIndex, matchLabel.Label));
            }
        }
    }
}