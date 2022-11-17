using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class MethodDescriptorEditorViewModel : BaseDialogViewModel {
        private TypeDescriptor returnType;
        public TypeDescriptor ReturnType {
            get => this.returnType;
            set => RaisePropertyChanged(ref this.returnType, value);
        }

        private TypeDescriptorViewModel selectedParameter;
        public TypeDescriptorViewModel SelectedParameter {
            get => this.selectedParameter;
            set => RaisePropertyChanged(ref this.selectedParameter, value);
        }

        public MethodDescriptor Descriptor => new MethodDescriptor(this.ReturnType, this.Parameters.Select(a => a.TypeDescriptor).ToList());

        public ObservableCollection<TypeDescriptorViewModel> Parameters { get; }

        public ICommand AddNewParameterCommand { get; }

        public ICommand RemoveSelectedCommand { get; }

        public ICommand EditReturnTypeCommand { get; }

        public MethodDescriptorEditorViewModel() {
            this.Parameters = new ObservableCollection<TypeDescriptorViewModel>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.AddNewParameterCommand = new RelayCommand(AddNewParameter);
            this.RemoveSelectedCommand = new RelayCommand(RemoveSelectedAction);
            this.EditReturnTypeCommand = new RelayCommand(EditReturnType);
        }

        public void EditReturnType() {
            if (Dialogs.TypeEditor.EditTypeDescriptorDialog(this.ReturnType, out TypeDescriptor descriptor).Result) {
                this.ReturnType = descriptor;
            }
        }

        public void AddNewParameter() {
            if (Dialogs.TypeEditor.EditTypeDescriptorDialog(new TypeDescriptor(PrimitiveType.Integer, 0), out TypeDescriptor descriptor).Result) {
                this.Parameters.Add(new TypeDescriptorViewModel() { TypeDescriptor = descriptor });
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