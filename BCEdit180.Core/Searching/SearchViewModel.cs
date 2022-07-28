using System;
using System.Diagnostics.SymbolStore;
using System.Windows.Input;
using BCEdit180.Core.Commands;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Searching {
    public class SearchViewModel : BaseViewModel, IDisposable {
        private string inputText;
        public string InputText {
            get => this.inputText;
            set {
                RaisePropertyChanged(ref this.inputText, value);
                if (CanSearchForInput()) {
                    this.SearchService.Bump();
                }
                else {
                    this.SearchService.CanFireNextTick = false;
                    this.SearchCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ExtendedRelayCommand SearchCommand { get; }

        public ICommand ClearSearchCommand { get; }

        public SearchService SearchService { get; }

        private bool wasLastSearchForced;
        public bool WasLastSearchForced => this.wasLastSearchForced;

        public SearchViewModel() {
            this.SearchService = new SearchService();
            this.SearchCommand = new ExtendedRelayCommand(ForceSearchAction, CanSearchForInput);
            this.ClearSearchCommand = new RelayCommand(ClearSearchAction);
        }

        public virtual void ForceSearchAction() {
            this.wasLastSearchForced = true;
            try {
                this.SearchService.ForceAction();
            }
            finally {
                this.wasLastSearchForced = false;
            }
        }

        public virtual void ClearSearchAction() {
            this.InputText = "";
        }

        public virtual bool CanSearchForInput() {
            return !string.IsNullOrEmpty(this.InputText);
        }

        public void Dispose() {
            this.SearchService.Dispose();
        }
    }
}