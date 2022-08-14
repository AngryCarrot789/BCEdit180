using JavaAsm.Instructions.Types;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class HandleViewModel : BaseViewModel {
        private Handle handle;
        public Handle Handle {
            get => this.handle;
            set => RaisePropertyChanged(ref this.handle, value);
        }

        public HandleViewModel() {

        }
    }
}