using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    /// <summary>
    /// Mainly used as a wrapper for strings, so that a list of text boxes 
    /// can be used (as string is immutable, but this class is mutable)
    /// </summary>
    /// <typeparam name="T">Type of object this stores</typeparam>
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
