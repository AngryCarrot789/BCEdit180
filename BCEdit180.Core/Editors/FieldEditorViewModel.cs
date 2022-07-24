using System;
using System.Windows.Input;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.FieldCreator {
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

        public void EditDescriptor() {
            ViewManager.ShowEditType((p, c, a) => {
                this.Descriptor = p.HasValue ? new TypeDescriptor(p.Value, a) : new TypeDescriptor(new ClassName(c), a);
            }, this.Descriptor?.PrimitiveType, this.Descriptor?.ClassName?.Name);
        }

        public void EditAccess() {
            ViewManager.ShowAccessEditor((a) => this.Access = a);
        }

        public void ApplyChanges() {
            this.Callback?.Invoke();
        }
    }
}