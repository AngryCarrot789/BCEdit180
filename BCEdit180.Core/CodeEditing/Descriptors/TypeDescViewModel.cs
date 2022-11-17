using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class TypeDescViewModel : BaseViewModel {
        private TypeDescriptor typeDescriptor;
        public TypeDescriptor TypeDescriptor {
            get => this.typeDescriptor;
            set => RaisePropertyChanged(ref this.typeDescriptor, value);
        }

        public ICommand EditFieldDescriptorCommand { get; }

        public TypeDescViewModel() : this(new TypeDescriptor(PrimitiveType.Integer, 0)) {

        }

        public TypeDescViewModel(TypeDescriptor typeDescriptor) {
            this.EditFieldDescriptorCommand = new RelayCommand(EditTypeDescriptorAction);
            this.TypeDescriptor = typeDescriptor;
        }

        public void EditTypeDescriptorAction() {
            if (DialogUtils.EditType(this.TypeDescriptor, out TypeDescriptor descriptor)) {
                this.TypeDescriptor = descriptor;
            }
        }
    }
}