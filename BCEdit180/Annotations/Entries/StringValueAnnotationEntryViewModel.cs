using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using JavaAsm.CustomAttributes.Annotation;

namespace BCEdit180.Annotations.Entries {
    public class StringValueAnnotationEntryViewModel : BaseAnnotationEntryViewModel {

        private string entryValue;
        public string EntryValue {
            get => this.entryValue;
            set => RaisePropertyChanged(ref this.entryValue, value);
        }

        public StringValueAnnotationEntryViewModel(AnnotationViewModel annotation, AnnotationNode.ElementValuePair entry) : base(annotation, entry) {

        }

        public override void Load(AnnotationNode.ElementValuePair pair) {
            base.Load(pair);
            switch (this.value.Tag) {
                case ElementValue.ElementValueTag.Byte:
                case ElementValue.ElementValueTag.Character:
                case ElementValue.ElementValueTag.Double:
                case ElementValue.ElementValueTag.Float:
                case ElementValue.ElementValueTag.Integer:
                case ElementValue.ElementValueTag.Long:
                case ElementValue.ElementValueTag.Short:
                case ElementValue.ElementValueTag.String:
                    this.EntryValue = this.value.ConstValue?.ToString() ?? "[null]";
                    break;
                case ElementValue.ElementValueTag.Boolean:
                case ElementValue.ElementValueTag.Enum:
                case ElementValue.ElementValueTag.Class:
                case ElementValue.ElementValueTag.Annotation:
                case ElementValue.ElementValueTag.Array:
                default:
                    throw new Exception("Unexpected element value tag type: " + this.value.Tag);
            }
        }

        public override void Save(AnnotationNode.ElementValuePair pair) {
            switch (this.value.Tag) {
                case ElementValue.ElementValueTag.Byte: this.value.ConstValue = pair.Value.ConstValue = byte.TryParse(this.EntryValue, out byte b) ? b : throw new Exception("Entry Value cannot be parsed back to a byte"); break;
                case ElementValue.ElementValueTag.Character: this.value.ConstValue = pair.Value.ConstValue = this.EntryValue.Length > 0 ? this.EntryValue[0] : throw new Exception("Entry Value is empty and cannot be a char"); break;
                case ElementValue.ElementValueTag.Double: this.value.ConstValue = pair.Value.ConstValue = double.TryParse(this.EntryValue, out double d) ? d : throw new Exception("Entry Value cannot be parsed back to a double"); break;
                case ElementValue.ElementValueTag.Float: this.value.ConstValue = pair.Value.ConstValue = float.TryParse(this.EntryValue, out float f) ? f : throw new Exception("Entry Value cannot be parsed back to a float"); break;
                case ElementValue.ElementValueTag.Integer: this.value.ConstValue = pair.Value.ConstValue = int.TryParse(this.EntryValue, out int i) ? i : throw new Exception("Entry Value cannot be parsed back to a int"); break;
                case ElementValue.ElementValueTag.Long: this.value.ConstValue = pair.Value.ConstValue = long.TryParse(this.EntryValue, out long l) ? l : throw new Exception("Entry Value cannot be parsed back to a long"); break;
                case ElementValue.ElementValueTag.Short: this.value.ConstValue = pair.Value.ConstValue = short.TryParse(this.EntryValue, out short s) ? s : throw new Exception("Entry Value cannot be parsed back to a short"); break;
                case ElementValue.ElementValueTag.Boolean: this.value.ConstValue = pair.Value.ConstValue = bool.TryParse(this.EntryValue, out bool bo) ? bo : throw new Exception("Entry Value cannot be parsed back to a boolean"); break;
                case ElementValue.ElementValueTag.String: this.value.ConstValue = pair.Value.ConstValue = this.EntryValue; break;
                case ElementValue.ElementValueTag.Enum:
                case ElementValue.ElementValueTag.Class:
                case ElementValue.ElementValueTag.Annotation:
                case ElementValue.ElementValueTag.Array:
                    // unsupported currently
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
