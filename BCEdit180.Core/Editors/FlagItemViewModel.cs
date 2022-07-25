using System;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class FlagItemViewModel : BaseViewModel {
        private string name;
        private bool isChecked;
        private readonly Action<FlagItemViewModel> onStateChanged;

        public long Bit { get; }

        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        public bool IsChecked {
            get => this.isChecked;
            set {
                RaisePropertyChanged(ref this.isChecked, value);
                this.onStateChanged(this);
            }
        }

        public FlagItemViewModel(string name, long bit, Action<FlagItemViewModel> onStateChanged) {
            this.Name = name;
            this.Bit = bit;
            this.onStateChanged = onStateChanged;
        }

        public override string ToString() {
            return $"FlagItemViewModel({this.Name} @ {this.Bit} -> {this.IsChecked})";
        }
    }
}