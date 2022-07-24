using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Dialogs;
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

        public ICommand ApplyChangesCommand { get; }

        public Action Callback { get; set; }

        public FieldEditorViewModel() {
            this.FieldName = "myFieldName";
            this.Descriptor = new TypeDescriptor(PrimitiveType.Integer, 0);
            this.Access = FieldAccessModifiers.Public;
            this.EditDescriptorCommand = new RelayCommand(()=> {
                EditDescriptor();
            });

            this.EditAccessCommand = new RelayCommand(() => {
                EditAccess();
            });

            this.ApplyChangesCommand = new RelayCommand(ApplyChanges);
        }

        public async Task EditDescriptor() {
            this.Descriptor = await Dialog.TypeEditor.EditTypeDescriptorDialog(this.Descriptor);
        }

        public async Task EditAccess() {
            this.Access = await Dialog.AccessEditor.EditFieldAccess(this.Access);
        }

        public void ApplyChanges() {
            this.Callback?.Invoke();
        }
    }
}