using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class FieldEditorViewModel : BaseViewModel {
        private string fieldName;
        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        private TypeDescriptor descriptor;
        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        private FieldAccessModifiers access;
        public FieldAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        private object constantValue;
        public object ConstantValue {
            get => this.constantValue;
            set => RaisePropertyChanged(ref this.constantValue, value);
        }

        public ICommand EditDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public ICommand EditConstValueCommand { get; }

        public FieldEditorViewModel() {
            this.FieldName = "myFieldName";
            this.Descriptor = new TypeDescriptor(PrimitiveType.Integer, 0);
            this.Access = FieldAccessModifiers.Public;
            this.EditDescriptorCommand = new RelayCommand(EditDescriptorAction);
            this.EditAccessCommand = new RelayCommand(EditAccessAction);
            this.EditConstValueCommand = new RelayCommand(EditConstValueAction);
        }

        public void EditDescriptorAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(this.Descriptor, out TypeDescriptor descriptor).Result) {
                this.Descriptor = descriptor;
            }
        }

        public void EditAccessAction() {
            if (Dialog.AccessEditor.EditFieldAccess(this.Access, out FieldAccessModifiers access).Result) {
                this.Access = access;
            }
        }

        public void EditConstValueAction() {
            ConstValueEditorViewModel vm = new ConstValueEditorViewModel(this.ConstantValue);
            vm.IsEnabledMethodDescriptor = false;
            vm.IsEnabledHandle = false;
            vm.IsEnabledClass = false;

            if (Dialog.TypeEditor.EditConstantDialog(vm, out ConstValueEditorViewModel editor).Result) {
                if (editor.CheckEnabledStatesWithDialog()) {
                    this.ConstantValue = editor.GetValue();
                }
            }
        }
    }
}