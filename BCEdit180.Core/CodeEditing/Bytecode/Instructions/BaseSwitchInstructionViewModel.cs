using System.Collections.ObjectModel;

namespace BCEdit180.Core.CodeEditing.Bytecode.Instructions {
    public abstract class BaseSwitchInstructionViewModel  : BaseInstructionViewModel, IBytecodeEditorAccess {
        private MatchLabelViewModel defaultLabel;
        public MatchLabelViewModel DefaultLabel {
            get => this.defaultLabel;
            set => RaisePropertyChanged(ref this.defaultLabel, value);
        }

        private long defaultIndex;
        public long DefaultIndex {
            get => this.defaultIndex;
            set => RaisePropertyChanged(ref this.defaultIndex, value);
        }

        public BytecodeEditorViewModel BytecodeEditor { get; set; }

        public ObservableCollection<MatchLabelViewModel> MatchLabels { get; }

        protected BaseSwitchInstructionViewModel() {
            this.MatchLabels = new ObservableCollection<MatchLabelViewModel>();
        }

        protected void SetCallbacks(MatchLabelViewModel match) {
            match.SelectLabelCallback = SelectLabel;
            match.EditTargetLabelCallback = EditLabelTarget;
        }

        private void SelectLabel(MatchLabelViewModel label) {
            if (label.Label != null) {
                this.BytecodeEditor.SelectLabel(label.Label);
            }
        }

        private void EditLabelTarget(MatchLabelViewModel label) {
            if (label.Label != null) {
                if (this.BytecodeEditor.EditBranchTargetActionWithDialog(out LabelViewModel target)) {
                    label.Label = target.Label;
                }
            }
        }
    }
}