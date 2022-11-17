using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class FieldEditorViewModel : BaseDialogViewModel {
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
            if (DialogUtils.EditType(this.Descriptor, out TypeDescriptor descriptor)) {
                this.Descriptor = descriptor;
            }
        }

        public void EditAccessAction() {
            if (DialogUtils.ShowFieldAcccessDialog(this.Access, out FieldAccessModifiers modifiers)) {
                this.Access = modifiers;
            }
        }

        public void EditConstValueAction() {
            ConstValueEditorViewModel vm = new ConstValueEditorViewModel(this.ConstantValue);
            vm.IsEnabledMethodDescriptor = false;
            vm.IsEnabledHandle = false;
            vm.IsEnabledClass = false;

            if (DialogUtils.EditConstantDialog(vm, out ConstValueEditorViewModel editor)) {
                if (editor.CheckEnabledStatesWithDialog()) {
                    if (editor.TryGetValue(out object value, out string error)) {
                        this.ConstantValue = value;
                    }
                    else if (error != null) {
                        Dialogs.Message.ShowMessage("Invalid value", error);
                    }
                }
            }
        }
    }
}