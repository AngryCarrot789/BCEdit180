using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.CodeEditing.Descriptors;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;

namespace BCEdit180.Core.Editors {
    public class MethodDescEditorViewModel : BaseDialogViewModel {
        private TypeDescriptor returnType;
        public TypeDescriptor ReturnType {
            get => this.returnType;
            set => RaisePropertyChanged(ref this.returnType, value);
        }

        private TypeDescViewModel selectedParameter;
        public TypeDescViewModel SelectedParameter {
            get => this.selectedParameter;
            set => RaisePropertyChanged(ref this.selectedParameter, value);
        }

        public MethodDescriptor Descriptor => new MethodDescriptor(this.ReturnType ?? new TypeDescriptor(PrimitiveType.Void, 0), this.Parameters.Select(a => a.TypeDescriptor).ToList());

        public ObservableCollection<TypeDescViewModel> Parameters { get; }

        public ICommand AddNewParameterCommand { get; }

        public ICommand RemoveSelectedCommand { get; }

        public ICommand EditReturnTypeCommand { get; }

        public MethodDescEditorViewModel() {
            this.Parameters = new ObservableCollection<TypeDescViewModel>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.AddNewParameterCommand = new RelayCommand(AddNewParameter);
            this.RemoveSelectedCommand = new RelayCommand(RemoveSelectedAction);
            this.EditReturnTypeCommand = new RelayCommand(EditReturnType);
        }

        public MethodDescEditorViewModel(MethodDescriptor desc) : this() {
            this.Parameters.AddAll(desc.ArgumentTypes.Select(a => new TypeDescViewModel(a)));
            this.ReturnType = desc.ReturnType;
        }

        public void EditReturnType() {
            if (DialogUtils.EditType(this.ReturnType, out TypeDescriptor descriptor)) {
                this.ReturnType = descriptor;
            }
        }

        public void AddNewParameter() {
            if (DialogUtils.EditType(new TypeDescriptor(PrimitiveType.Integer, 0), out TypeDescriptor descriptor)) {
                this.Parameters.Add(new TypeDescViewModel(descriptor));
            }
        }

        public void RemoveSelectedAction() {
            if (this.SelectedParameter != null) {
                this.Parameters.Remove(this.SelectedParameter);
                if (this.Parameters.Count > 0) {
                    this.SelectedParameter = this.Parameters[this.Parameters.Count - 1];
                }
            }
        }
    }
}