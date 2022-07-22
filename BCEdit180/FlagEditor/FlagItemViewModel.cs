using System;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.FlagEditor {
    public class FlagItemViewModel : BaseViewModel {
        private string name;
        private bool isChecked;

        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        public bool IsChecked {
            get => this.isChecked;
            set {
                RaisePropertyChanged(ref this.isChecked, value);
                this.OnStateChanged(this);
            }
        }

        public long Bit { get; }

        private Action<FlagItemViewModel> OnStateChanged { get; }

        public FlagItemViewModel(string name, long bit, Action<FlagItemViewModel> onStateChanged) {
            this.Name = name;
            this.Bit = bit;
            this.OnStateChanged = onStateChanged;
        }

        public override string ToString() {
            return $"FlagItemViewModel({this.Name} @ {this.Bit} -> {this.IsChecked})";
        }
    }
}