using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Window {
    public class ErrorDialogViewModel : BaseViewModel {
        private string description;
        public string Description {
            get => this.description;
            set => RaisePropertyChanged(ref this.description, value);
        }
    }
}
