using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing {
    public class OpcodeDescriptorViewModel : BaseViewModel {
        private string header;
        public string Header {
            get => this.header;
            set => RaisePropertyChanged(ref this.header, value);
        }

        private string stackTransition;
        public string StackTransition {
            get => this.stackTransition;
            set => RaisePropertyChanged(ref this.stackTransition, value);
        }

        private string description;
        public string Description {
            get => this.description;
            set => RaisePropertyChanged(ref this.description, value);
        }
    }
}
