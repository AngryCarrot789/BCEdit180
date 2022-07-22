using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ReferenceObjectViewModel : BaseViewModel {
        private object value;
        public object Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public ReferenceObjectViewModel() {

        }

        public ReferenceObjectViewModel(object initialValue) {
            this.Value = initialValue;
        }
    }
}
