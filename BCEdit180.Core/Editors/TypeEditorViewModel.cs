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
                UpdateClassName(true);
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
                UpdateClassName(true);
            }
        }

        private bool isPrimitive;
        public bool IsPrimitive {
            get => this.isPrimitive;
            set => RaisePropertyChanged(ref this.isPrimitive, value);
        }

        private ushort arrayDepth;
        public ushort ArrayDepth {
            get => this.arrayDepth;
            set {
                RaisePropertyChanged(ref this.arrayDepth, value);
                UpdateClassName(false);
            }
        }

        private bool allowPrimitive;
        public bool AllowPrimitive {
            get => this.allowPrimitive;
            set => RaisePropertyChanged(ref this.allowPrimitive, value);
        }

        private bool allowClass;
        public bool AllowClass {
            get => this.allowClass;
            set => RaisePropertyChanged(ref this.allowClass, value);
        }

        public TypeEditorViewModel() {
            this.ArrayDepth = 0;
            this.AllowPrimitive = true;
            this.AllowClass = true;
        }

        private bool isUpdating;
        public void UpdateClassName(bool calculateArrayDepth) {
            if (this.isUpdating) {
                return;
            }

            this.isUpdating = true;

            if (calculateArrayDepth) {
                this.ArrayDepth = (ushort) (string.IsNullOrEmpty(this.ClassName) ? 0 : this.ClassName.CountCharsAtStart('['));
            }

            string arrayParts = '['.Repeat(this.ArrayDepth);
            if (string.IsNullOrEmpty(this.ClassName)) {
                this.RealClassName = arrayParts;
            }
            else {
                this.RealClassName = arrayParts + GetActualClassName(this.ClassName);
            }

            this.isUpdating = false;
        }

        public string GetActualClassName(string clazz) {
            clazz = clazz.Replace('.', '/');
            if (clazz.Length > 0 && clazz[0] == 'L' && clazz[clazz.Length - 1] == ';') {
                clazz = clazz.Substring(1, clazz.Length - 2);
            }

            return clazz;
        }

        public string GetClassName() {
            return GetActualClassName(this.ClassName);
        }
    }
}
