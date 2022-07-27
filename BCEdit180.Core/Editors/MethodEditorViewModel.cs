using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.Editors {
    public class MethodEditorViewModel : MethodDescEditorViewModel {
        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private MethodAccessModifiers access;
        public MethodAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        public ICommand EditAccessCommand { get; }

        public MethodEditorViewModel() : base() {
            this.MethodName = "methodName";
            this.Access = MethodAccessModifiers.Public;
            this.EditAccessCommand = new RelayCommand(EditAccess);
        }

        public void EditAccess() {
            if (Dialog.AccessEditor.EditMethodAccess(this.Access, out MethodAccessModifiers access).Result) {
                this.Access = access;
            }
        }
    }
}
