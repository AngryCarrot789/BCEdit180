using System;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Dialogs {
    public class DialogMessageViewModel : BaseViewModel {
        private string headerMessage;
        public string HeaderMessage {
            get => this.headerMessage;
            set => RaisePropertyChanged(ref this.headerMessage, value);
        }

        private string description;
        public string Description {
            get => this.description;
            set => RaisePropertyChanged(ref this.description, value);
        }

        private bool isLoading;
        public bool IsLoading {
            get => this.isLoading;
            set => RaisePropertyChanged(ref this.isLoading, value);
        }

        public Action CloseCallback { get; }

        public DialogMessageViewModel(Action close) {
            this.CloseCallback = close;
        }

        public void CloseDialog() {
            this.CloseCallback();
        }
    }
}
