using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class TypeDescriptorViewModel : BaseViewModel {
        private TypeDescriptor typeDescriptor;
        public TypeDescriptor TypeDescriptor {
            get => this.typeDescriptor;
            set => RaisePropertyChanged(ref this.typeDescriptor, value);
        }

        public ICommand EditFieldDescriptorCommand { get; }

        public TypeDescriptorViewModel() : this(new TypeDescriptor(PrimitiveType.Integer, 0)) {

        }

        public TypeDescriptorViewModel(TypeDescriptor typeDescriptor) {
            this.EditFieldDescriptorCommand = new RelayCommand(EditTypeDescriptorAction);
            this.TypeDescriptor = typeDescriptor;
        }

        public void EditTypeDescriptorAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.TypeDescriptor, out TypeDescriptor descriptor).Result) {
                this.TypeDescriptor = descriptor;
            }
        }
    }
}