using System;
using System.Windows.Input;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class TypeEditorViewModel : BaseViewModel {
        private string className;
        public string ClassName {
            get => this.className;
            set {
                RaisePropertyChanged(ref this.className, value);
                UpdateClassName();
            }
        }
        
        private string realClassName;
        public string RealClassName {
            get => this.realClassName;
            set => RaisePropertyChanged(ref this.realClassName, value);
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

        private ushort arrayDepth;
        public ushort ArrayDepth {
            get => this.arrayDepth;
            set => RaisePropertyChanged(ref this.arrayDepth, value);
        }

        public ICommand ApplyChangesCommand { get; }

        public Action<PrimitiveType?, string, int> Callback { get; set; }

        public TypeEditorViewModel() {
            this.ApplyChangesCommand = new RelayCommand(ApplyChange);
        }

        public void ApplyChange() {
            if (this.IsPrimitive) {
                this.Callback?.Invoke(this.SelectedPrimitive, null, this.ArrayDepth);
            }
            else if (!string.IsNullOrEmpty(this.ClassName)) {
                this.Callback?.Invoke(null, this.ClassName, this.ArrayDepth);
            }
        }

        public void UpdateClassName() {
            if (string.IsNullOrEmpty(this.ClassName)) {
                this.RealClassName = "";
            }
            else {
                string clazz = this.ClassName.Replace('.', '/');
                if (clazz.Length > 0 && clazz[0] == 'L' && clazz[clazz.Length - 1] == ';') {
                    clazz = clazz.Substring(1, clazz.Length - 2);
                }

                this.RealClassName = clazz;
            }
        }
    }
}
