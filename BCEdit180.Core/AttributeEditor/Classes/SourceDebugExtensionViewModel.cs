using BCEdit180.Core;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.AttributeEditor.Classes {
    public class SourceDebugExtensionViewModel : BaseViewModel, ISaveable<ClassNode> {
        private string value;
        public string Value {
            get => this.value;
            set => RaisePropertyChanged(ref this.value, value);
        }

        public void Load(ClassNode node) {
            this.Value = node.SourceDebugExtension;
        }

        public void Save(ClassNode node) {
            node.SourceDebugExtension = string.IsNullOrWhiteSpace(this.Value) ? null : this.Value;
        }
    }
}