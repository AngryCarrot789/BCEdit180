using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;

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

        private MatchLabelViewModel selectedLabel;
        public MatchLabelViewModel SelectedLabel {
            get => this.selectedLabel;
            set => RaisePropertyChanged(ref this.selectedLabel, value);
        }

        public RelayCommand RemoveSelectedLabelCommand { get; }
        public RelayCommandParam<MatchLabelViewModel> RemoveSelfCommand { get; }

        protected BaseSwitchInstructionViewModel() {
            this.MatchLabels = new ObservableCollection<MatchLabelViewModel>();
            this.RemoveSelectedLabelCommand = new RelayCommand(RemoveSelectedLabelAction);
            this.RemoveSelfCommand = new RelayCommandParam<MatchLabelViewModel>(RemoveSelfAction);
        }

        protected void SetCallbacks(MatchLabelViewModel match) {
            match.SelectLabelCallback = SelectLabel;
            match.EditTargetLabelCallback = EditLabelTarget;
            match.RemoveSelfCallback = RemoveSelfAction;
            match.UpdateErrrosCallback = (t) => UpdateErrors();
        }

        private void RemoveSelfAction(MatchLabelViewModel label) {
            this.MatchLabels.Remove(label);
            UpdateErrors();
        }

        private void RemoveSelectedLabelAction() {
            if (this.SelectedLabel != null) {
                this.MatchLabels.Remove(this.SelectedLabel);
            }

            UpdateErrors();
        }

        private void UpdateErrors() {
            if (this is TableSwitchInstructionViewModel tableSwitch) {
                HashSet<int> set = new HashSet<int>();
                for (int i = tableSwitch.LowValue; i <= tableSwitch.HighValue; i++) {
                    set.Add(i);
                }

                foreach (MatchLabelViewModel label in this.MatchLabels) {
                    if (set.Contains(label.SwitchIndex)) {
                        continue;
                    }
                    else if (label.SwitchIndex > tableSwitch.HighValue) {
                        MessageDispatcher.Publish(new AddMessage() {Message = $"SwitchTable case {label.SwitchIndex} is too high; it's outside the range {tableSwitch.LowValue}-{tableSwitch.HighValue}"});
                    }
                    else if (label.SwitchIndex < tableSwitch.LowValue) {
                        MessageDispatcher.Publish(new AddMessage() {Message = $"SwitchTable case {label.SwitchIndex} is too low; it's outside the range {tableSwitch.LowValue}-{tableSwitch.HighValue}"});
                    }
                    else {
                        // ??????????????????????
                        MessageDispatcher.Publish(new AddMessage() {Message = $"SwitchTable is missing case {label.SwitchIndex}"});
                    }

                    return;
                }

                Dictionary<int, MatchLabelViewModel> map = new Dictionary<int, MatchLabelViewModel>();
                foreach (MatchLabelViewModel label in this.MatchLabels) {
                    map[label.SwitchIndex] = label;
                }

                foreach (int value in set) {
                    if (!map.ContainsKey(value)) {
                        MessageDispatcher.Publish(new AddMessage() { Message = $"SwitchTable is missing case {value}; this table requires the range {tableSwitch.LowValue}-{tableSwitch.HighValue}" });
                        return;
                    }
                }

                MessageDispatcher.Publish(new AddMessage() {Message = ""});
            }
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