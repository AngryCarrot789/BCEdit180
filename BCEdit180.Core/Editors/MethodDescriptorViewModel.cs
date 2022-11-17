using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class MethodDescriptorViewModel : BaseDialogViewModel, IMethodDescriptable {
        private MethodDescriptor methodDescriptor;
        public MethodDescriptor MethodDescriptor {
            get => this.methodDescriptor;
            set => RaisePropertyChanged(ref this.methodDescriptor, value);
        }

        public ICommand EditMethodDescriptorCommand { get; }

        public MethodDescriptorViewModel() {
            this.EditMethodDescriptorCommand = new RelayCommand(EditDescriptor);
        }

        public void EditDescriptor() {
            if (DialogUtils.EditMethodDesc(this.MethodDescriptor, out MethodDescriptor typeDesc)) {
                this.MethodDescriptor = typeDesc;
            }
        }
    }
}