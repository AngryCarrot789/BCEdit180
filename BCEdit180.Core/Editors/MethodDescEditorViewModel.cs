using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class MethodDescEditorViewModel : BaseViewModel {
        private TypeDescriptor returnType;
        public TypeDescriptor ReturnType {
            get => this.returnType;
            set => RaisePropertyChanged(ref this.returnType, value);
        }

        public ObservableCollection<TypeDescriptorViewModel> Parameters { get; }

        private MethodAccessModifiers access;
        public MethodAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        public ICommand EditReturnTypeCommand { get; }

        public ICommand AddNewParameterCommand { get; }

        public ICommand EditAccessCommand { get; }

        public MethodDescEditorViewModel() {
            this.Parameters = new ObservableCollection<TypeDescriptorViewModel>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.Access = MethodAccessModifiers.Public;
            this.AddNewParameterCommand = new RelayCommand(AddNewParameter);
            this.EditReturnTypeCommand = new RelayCommand(EditReturnType);
            this.EditAccessCommand = new RelayCommand(EditAccess);
        }

        public void EditReturnType() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.ReturnType, out TypeDescriptor descriptor).Result) {
                this.ReturnType = descriptor;
            }
        }

        public void AddNewParameter() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(new TypeDescriptor(PrimitiveType.Integer, 0), out TypeDescriptor descriptor).Result) {
                this.Parameters.Add(new TypeDescriptorViewModel() {
                    Descriptor = descriptor
                });
            }
        }

        public void EditAccess() {
            if (Dialog.AccessEditor.EditMethodAccess(this.Access, out MethodAccessModifiers access).Result) {
                this.Access = access;
            }
        }
    }
}