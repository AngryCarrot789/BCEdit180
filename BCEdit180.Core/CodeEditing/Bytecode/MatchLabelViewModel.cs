using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode {
    public class MatchLabelViewModel : BaseViewModel {
        private int index;
        public int Index {
            get => this.index;
            set => RaisePropertyChanged(ref this.index, value);
        }

        private long labelIndex;
        public long LabelIndex {
            get => this.labelIndex;
            set => RaisePropertyChanged(ref this.labelIndex, value);
        }

        public override string ToString() {
            return $"{this.Index} -> {this.LabelIndex}";
        }
    }
}