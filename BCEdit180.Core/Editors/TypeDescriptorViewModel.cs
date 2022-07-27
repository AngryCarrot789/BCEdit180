using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class TypeDescriptorViewModel : BaseViewModel {
        private TypeDescriptor descriptor;
        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public ICommand EditCommand { get; }

        public TypeDescriptorViewModel() : this(new TypeDescriptor(PrimitiveType.Integer, 0)) {

        }

        public TypeDescriptorViewModel(TypeDescriptor descriptor) {
            this.EditCommand = new RelayCommand(EditAction);
            this.Descriptor = descriptor;
        }

        public void EditAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.Descriptor, out TypeDescriptor descriptor).Result) {
                this.Descriptor = descriptor;
            }
        }
    }
}