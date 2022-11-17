using System;
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
                    this.IdleEventService.OnInput();
                }
                else {
                    OnSearchReset();
                }
            }
        }

        public ExtendedRelayCommand SearchCommand { get; }

        public ICommand ClearSearchCommand { get; }

        public IdleEventService IdleEventService { get; }

        public bool WasLastSearchForced { get; private set; }

        public SearchViewModel() {
            this.IdleEventService = new IdleEventService();
            this.SearchCommand = new ExtendedRelayCommand(ForceSearchAction, CanSearchForInput);
            this.ClearSearchCommand = new RelayCommand(ClearSearchInputAction);
        }

        public virtual void ForceSearchAction() {
            this.WasLastSearchForced = true;
            try {
                this.IdleEventService.ForceAction();
            }
            finally {
                this.WasLastSearchForced = false;
            }
        }

        public virtual void ClearSearchInputAction() {
            this.InputText = "";
        }

        public virtual bool CanSearchForInput() {
            return !string.IsNullOrEmpty(this.InputText);
        }

        /// <summary>
        /// Called when the search is no longer active; reset everything to it's original state
        /// </summary>
        public virtual void OnSearchReset() {
            this.IdleEventService.CanFireNextTick = false;
            this.SearchCommand.RaiseCanExecuteChanged();
        }

        public void Dispose() {
            this.IdleEventService.Dispose();
        }
    }
}