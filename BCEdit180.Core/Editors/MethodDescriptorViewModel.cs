using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class MethodDescriptorWViewModel : BaseViewModel, IMethodDescriptable {
        private MethodDescriptor methodDescriptor;
        public MethodDescriptor MethodDescriptor {
            get => this.methodDescriptor;
            set => RaisePropertyChanged(ref this.methodDescriptor, value);
        }

        public ICommand EditMethodDescriptorCommand { get; }

        public MethodDescriptorWViewModel() {
            this.EditMethodDescriptorCommand = new RelayCommand(EditDescriptor);
        }

        public void EditDescriptor() {
            if (Dialog.TypeEditor.EditMethodDescriptorDialog(this.MethodDescriptor, out MethodDescriptor typeDesc).Result) {
                this.MethodDescriptor = typeDesc;
            }
        }
    }
}