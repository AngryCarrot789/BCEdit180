using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ReferenceObjectViewModel<T> : BaseViewModel {
        private T value;
        public T Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public ReferenceObjectViewModel() {

        }

        public ReferenceObjectViewModel(T initialValue) {
            this.Value = initialValue;
        }
    }
}
