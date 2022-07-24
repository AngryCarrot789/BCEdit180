using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BCEdit180.Core.Dialogs;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.MethodCreator {
    public class MethodEditorViewModel : BaseViewModel {
        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

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

        public ICommand ApplyChangesCommand { get; }

        public ICommand EditAccessCommand { get; }

        public Action Callback { get; set; }

        public MethodEditorViewModel() {
            this.MethodName = "methodName";
            this.Parameters = new ObservableCollection<TypeDescriptorViewModel>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.Access = MethodAccessModifiers.Public;
            this.AddNewParameterCommand = new RelayCommand(() => {
                AddNewParameter();
            });

            this.EditReturnTypeCommand = new RelayCommand(()=> {
                EditReturnType();
            });

            this.EditAccessCommand = new RelayCommand(() => {
                EditAccess();
            });

            this.ApplyChangesCommand = new RelayCommand(ApplyChanges);
        }

        public async void EditReturnType() {
            this.ReturnType = await Dialog.TypeEditor.EditTypeDescriptorDialog(this.ReturnType);
        }

        public async void AddNewParameter() {
            TypeDescriptor descriptor = await Dialog.TypeEditor.EditTypeDescriptorDialog(new TypeDescriptor(PrimitiveType.Integer, 0));
            this.Parameters.Add(new TypeDescriptorViewModel() {
                Descriptor = descriptor
            });
        }

        public void EditAccess() {
            ViewManager.ShowAccessEditor((a) => this.Access = a);
        }

        public void ApplyChanges() {
            this.Callback?.Invoke();
        }
    }
}
