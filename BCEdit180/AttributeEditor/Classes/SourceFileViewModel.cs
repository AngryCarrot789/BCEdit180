using System.Threading;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.AttributeEditor.Classes {
    public class SourceFileViewModel : BaseViewModel, ISaveable<ClassNode> {
        private string sourceFile;
        public string SourceFile {
            get => this.sourceFile;
            set => RaisePropertyChanged(ref this.sourceFile, value);
        }

        public void Load(ClassNode node) {
            this.SourceFile = node.SourceFile;
        }

        public void Save(ClassNode node) {
            node.SourceFile = this.SourceFile;
        }
    }
}