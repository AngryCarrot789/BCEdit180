using System.Text;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Utils;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors {
    public class TypeEditorViewModel : BaseDialogViewModel {
        private string inputClassName;
        public string InputClassName {
            get => this.inputClassName;
            set {
                RaisePropertyChanged(ref this.inputClassName, value);
                UpdateClassName(true);
            }
        }
        
        private string previewInternalName;
        public string PreviewInternalName {
            get => this.previewInternalName;
            set => RaisePropertyChanged(ref this.previewInternalName, value);
        }

        private string previewDescriptor;
        public string PreviewDescriptor {
            get => this.previewDescriptor;
            set => RaisePropertyChanged(ref this.previewDescriptor, value);
        }

        private string previewClassName;
        public string PreviewClassName {
            get => this.previewClassName;
            set => RaisePropertyChanged(ref this.previewClassName, value);
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

        public string ArrayPartString => '['.Repeat(this.ArrayDepth);

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
                this.ArrayDepth = (ushort) (string.IsNullOrEmpty(this.InputClassName) ? 0 : this.InputClassName.CountCharsAtStart('['));
            }

            string arrayParts = this.ArrayPartString;
            if (string.IsNullOrEmpty(this.InputClassName)) {
                this.PreviewInternalName = arrayParts;
                this.PreviewClassName = "";
                this.PreviewDescriptor = arrayParts;
            }
            else {
                string strippedArray = StripArrayDepthPart(this.InputClassName);
                this.PreviewInternalName = GetInternalName(strippedArray);
                this.PreviewClassName = GetClassName(strippedArray);
                this.PreviewDescriptor = arrayParts + GetDescriptor(strippedArray);
            }

            this.isUpdating = false;
        }

        private static string StripDescriptorElements(string input) {
            if (input.Length > 0 && input[0] == 'L' && input[input.Length - 1] == ';') {
                input = input.Substring(1, input.Length - 2);
            }

            return input;
        }

        private static string StripArrayDepthPart(string input) {
            int i = 0, len = input.Length;
            while (i < len && input[i] == '[') { i++; }
            return i == 0 ? input : input.Substring(i);
        }

        public string GetInternalName(string input) {
            return StripDescriptorElements(StripArrayDepthPart(input.Replace('.', '/')));
        }

        public string GetClassName(string input) {
            return StripDescriptorElements(StripArrayDepthPart(input.Replace('/', '.')));
        }

        public string GetDescriptor(string input) {
            string name = StripArrayDepthPart(input).Replace('.', '/');
            if (string.IsNullOrWhiteSpace(name)) {
                return "";
            }

            if (name[0] != 'L') {
                if (name[name.Length - 1] != ';') {
                    name = ("L" + name + ";");
                }
                else {
                    name = ("L" + name);
                }
            }
            else if (name[name.Length - 1] != ';') {
                name += ";";
            }

            return name;
        }

        public string GetInternalName() {
            return GetInternalName(this.InputClassName);
        }

        public string GetClassName() {
            return GetClassName(this.InputClassName);
        }

        public string GetDescriptor() {
            return GetDescriptor(this.InputClassName);
        }
    }
}
