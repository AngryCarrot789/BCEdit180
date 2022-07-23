using System;
using System.Windows.Input;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.TypeEditor {
    public class TypeEditorViewModel : BaseViewModel {
        private string className;
        public string ClassName {
            get => this.className;
            set => RaisePropertyChanged(ref this.className, value);
        }

        private PrimitiveType selectedPrimitive;
        public PrimitiveType SelectedPrimitive {
            get => this.selectedPrimitive;
            set => RaisePropertyChanged(ref this.selectedPrimitive, value);
        }

        private bool isObject;
        public bool IsObject {
            get => this.isObject;
            set => RaisePropertyChanged(ref this.isObject, value);
        }

        private bool isPrimitive;
        public bool IsPrimitive {
            get => this.isPrimitive;
            set => RaisePropertyChanged(ref this.isPrimitive, value);
        }

        public ICommand ApplyChangesCommand { get; }

        public Action<PrimitiveType?, string> Callback { get; set; }

        public TypeEditorViewModel() {
            this.ApplyChangesCommand = new RelayCommand(this.ApplyChange);
        }

        public void ApplyChange() {
            if (this.IsPrimitive) {
                this.Callback?.Invoke(this.SelectedPrimitive, null);
            }
            else if (this.ClassName != null && this.ClassName.Length > 0) {
                this.Callback?.Invoke(null, this.ClassName);
            }
        }
    }
}
