using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        public ObservableCollection<TypeDescriptor> Parameters { get; }

        public ICommand EditReturnTypeCommand { get; }

        public ICommand AddNewReturnTypeCommand { get; }

        public ICommand ApplyChangesCommand { get; }

        public Action<MethodDescriptor> Callback { get; set; }

        public MethodEditorViewModel() {
            this.MethodName = "methodName";
            this.Parameters = new ObservableCollection<TypeDescriptor>();
            this.ReturnType = new TypeDescriptor(PrimitiveType.Void, 0);
            this.AddNewReturnTypeCommand = new RelayCommand(() => {
                AddNewParameter();
            });

            this.EditReturnTypeCommand = new RelayCommand(()=> {
                this.EditReturnType();
            });

            this.ApplyChangesCommand = new RelayCommand(this.ApplyChanges);
        }

        public void EditReturnType() {
            ViewManager.ShowEditType((p, c) => {
                if (p.HasValue) {
                    this.ReturnType = new TypeDescriptor(p.Value, 0);
                }
                else {
                    this.ReturnType = new TypeDescriptor(new ClassName(c), 0);
                }
            }, this.returnType?.PrimitiveType, this.returnType?.ClassName.Name);
        }

        public void AddNewParameter() {
            ViewManager.ShowEditType((p, c) => {
                if (p.HasValue) {
                    this.Parameters.Add(new TypeDescriptor(p.Value, 0));
                }
                else {
                    this.Parameters.Add(new TypeDescriptor(new ClassName(c), 0));
                }
            }, PrimitiveType.Integer);
        }

        public void ApplyChanges() {
            this.Callback?.Invoke(new MethodDescriptor(this.ReturnType, new List<TypeDescriptor>(this.Parameters)));
        }
    }
}
