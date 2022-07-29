using System;
using System.Windows.Input;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode {
    public class MatchLabelViewModel : BaseViewModel {
        private int switchIndex;
        public int SwitchIndex {
            get => this.switchIndex;
            set => RaisePropertyChanged(ref this.switchIndex, value);
        }

        private long labelIndex;
        public long LabelIndex {
            get => this.labelIndex;
            set => RaisePropertyChanged(ref this.labelIndex, value);
        }

        private Label label;
        public Label Label {
            get => this.label;
            set {
                this.label = value;
                this.LabelIndex = value?.Index ?? -1;
            }
        }

        public ICommand SelectJumpDestinationCommand { get; set; }
        public ICommand EditTargetLabelCommand { get; set; }

        public Action<MatchLabelViewModel> SelectLabelCallback { get; set; }
        public Action<MatchLabelViewModel> EditTargetLabelCallback { get; set; }

        public MatchLabelViewModel() {
            this.SelectJumpDestinationCommand = new RelayCommand(() => this.SelectLabelCallback?.Invoke(this));
            this.EditTargetLabelCommand = new RelayCommand(() => this.EditTargetLabelCallback?.Invoke(this));
        }

        public override string ToString() {
            return $"SWITCH[{this.SwitchIndex}] -> L{this.LabelIndex}";
        }

        public void Load(in int index, in Label target) {
            this.Label = target;
            this.LabelIndex = target.Index;
            this.SwitchIndex = index;
        }
    }
}