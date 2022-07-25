using BCEdit180.Core.Utils;
using JavaAsm;
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
            set {
                RaisePropertyChanged(ref this.isObject, value);
                UpdateClassName();
            }
        }

        private bool isPrimitive;
        public bool IsPrimitive {
            get => this.isPrimitive;
            set => RaisePropertyChanged(ref this.isPrimitive, value);
        }

        private bool ignoreArrayDepthUpdate;
        private ushort arrayDepth;
        public ushort ArrayDepth {
            get => this.arrayDepth;
            set {
                RaisePropertyChanged(ref this.arrayDepth, value);
                if (!this.ignoreArrayDepthUpdate)
                    UpdateClassName();
            }
        }

        public TypeEditorViewModel() {
            this.ArrayDepth = 0;
        }

        public void UpdateClassName() {
            this.ignoreArrayDepthUpdate = true;
            this.ArrayDepth = (ushort) (string.IsNullOrEmpty(this.ClassName) ? 0 : this.ClassName.CountCharsAtStart('['));
            this.ignoreArrayDepthUpdate = false;
            string arrayParts = '['.Repeat(this.ArrayDepth);
            if (string.IsNullOrEmpty(this.ClassName)) {
                this.RealClassName = arrayParts;
            }
            else {
                string clazz = this.ClassName.Replace('.', '/');
                if (clazz.Length > 0 && clazz[0] == 'L' && clazz[clazz.Length - 1] == ';') {
                    clazz = clazz.Substring(1, clazz.Length - 2);
                }

                this.RealClassName = arrayParts + clazz;
            }
        }
    }
}
