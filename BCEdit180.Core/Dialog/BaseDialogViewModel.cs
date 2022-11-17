using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Dialog {
    public class BaseDialogViewModel : BaseViewModel {
        private string title;
        public string Title {
            get => this.title;
            set => RaisePropertyChanged(ref this.title, value);
        }
    }
}