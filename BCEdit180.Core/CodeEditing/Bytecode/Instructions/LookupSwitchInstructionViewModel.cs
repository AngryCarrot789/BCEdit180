using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BCEdit180.Core.Utils;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public class LookupSwitchInstructionViewModel : BaseInstructionViewModel {
        public override IEnumerable<Opcode> AvailableOpCodes => new Opcode[] {Opcode.LOOKUPSWITCH};

        public override bool CanEditOpCode => false;

        private long defaultIndex;
        public long DefaultIndex {
            get => this.defaultIndex;
            set => RaisePropertyChanged(ref this.defaultIndex, value);
        }

        private string matchLabelsString;
        public string MatchLabelsString {
            get => this.matchLabelsString;
            set => RaisePropertyChanged(ref this.matchLabelsString, value);
        }

        public List<MatchLabelViewModel> MatchLabels { get; }

        public LookupSwitchInstructionViewModel() {
            this.MatchLabels = new List<MatchLabelViewModel>();
        }

        public override void Load(Instruction instruction) {
            base.Load(instruction);
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;
            this.DefaultIndex = insn.Default?.Index ?? 0;
            this.MatchLabels.Clear();
            this.MatchLabels.AddAll(insn.MatchLabels.Select(a => new MatchLabelViewModel(){Index = a.Key, LabelIndex = a.Value.Index }));
            this.MatchLabelsString = string.Join(" | ", insn.MatchLabels.Select(a => $"{a.Key} -> {a.Value?.Index ?? -1}"));
        }

        public override void Save(Instruction instruction) {
            LookupSwitchInstruction insn = (LookupSwitchInstruction) instruction;
            base.Save(instruction);
        }
    }
}