using System;
using BCEdit180.Core.Dialog;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class HandleViewModel : BaseDialogViewModel {
        private Handle handle;
        public Handle Handle {
            get => this.handle;
            set => RaisePropertyChanged(ref this.handle, value);
        }

        public HandleViewModel() {
        }
    }
}