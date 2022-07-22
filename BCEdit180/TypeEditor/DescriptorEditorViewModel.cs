using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.TypeEditor {
    public class DescriptorEditorViewModel : BaseViewModel {
        public TypeDescriptor Descriptor { get; }

        public PrimitiveSelectorViewModel PrimitiveSelector { get; }

        private string typeName;
        public string TypeName {
            get => this.typeName;
            set {
                RaisePropertyChanged(ref this.typeName, value);
                UpdateRadioButtonStates();
            }
        }

        public DescriptorEditorViewModel(TypeDescriptor descriptor) {
            this.Descriptor = descriptor;
            this.PrimitiveSelector = new PrimitiveSelectorViewModel();
            Load(descriptor);
        }

        public void UpdateRadioButtonStates() {
            if (string.IsNullOrEmpty(this.typeName)) {
                this.PrimitiveSelector.SetAll(false);
                this.PrimitiveSelector.SelectedObject = true;
            }
            else if (this.typeName.Length == 1) {
                this.PrimitiveSelector.SetAll(false);
                switch (this.typeName[0]) {
                    case 'B': this.PrimitiveSelector.SelectedByte = true; break;
                    case 'C': this.PrimitiveSelector.SelectedChar = true; break;
                    case 'D': this.PrimitiveSelector.SelectedDouble = true; break;
                    case 'F': this.PrimitiveSelector.SelectedFloat = true; break;
                    case 'I': this.PrimitiveSelector.SelectedInt = true; break;
                    case 'J': this.PrimitiveSelector.SelectedLong = true; break;
                    case 'L': this.PrimitiveSelector.SelectedObject = true; break;
                    case 'S': this.PrimitiveSelector.SelectedShort = true; break;
                    case 'V': this.PrimitiveSelector.SelectedVoid = true; break;
                    case 'Z': this.PrimitiveSelector.SelectedBool = true; break;
                    default:
                        break;
                }
            }
            else if (this.typeName[0] == 'L') {
                this.PrimitiveSelector.SetAll(false);
                this.PrimitiveSelector.SelectedObject = true;
            }
            else {
                this.PrimitiveSelector.SetAll(false);
            }
        }

        public void Load(TypeDescriptor descriptor) {
            this.PrimitiveSelector.SetAll(false);
            if (descriptor.ClassName == null) {
                if (descriptor.PrimitiveType == null) {
                    return;
                }

                switch (descriptor.PrimitiveType) {
                    case PrimitiveType.Byte: this.PrimitiveSelector.SelectedByte = true; break;
                    case PrimitiveType.Character: this.PrimitiveSelector.SelectedChar = true; break;
                    case PrimitiveType.Double: this.PrimitiveSelector.SelectedDouble = true; break;
                    case PrimitiveType.Float: this.PrimitiveSelector.SelectedFloat = true; break;
                    case PrimitiveType.Integer: this.PrimitiveSelector.SelectedInt = true; break;
                    case PrimitiveType.Long: this.PrimitiveSelector.SelectedLong = true; break;
                    case PrimitiveType.Short: this.PrimitiveSelector.SelectedShort = true; break;
                    case PrimitiveType.Void: this.PrimitiveSelector.SelectedVoid = true; break;
                    case PrimitiveType.Boolean: this.PrimitiveSelector.SelectedBool = true; break;
                    default:
                        return;
                }
            }
            else {
                this.PrimitiveSelector.SelectedObject = true;
            }
        }

        public void Save(TypeDescriptor descriptor) {
            
        }
    }
}
