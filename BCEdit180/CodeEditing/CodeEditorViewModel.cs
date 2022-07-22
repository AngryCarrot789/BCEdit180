using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing {
    public class CodeEditorViewModel : BaseViewModel {
        public MethodNode Method { get; private set; }

        public BytecodeEditorViewModel ByteCodeEditor { get; }

        public ExceptionTableViewModel ExceptionEditor { get; }

        public CodeEditorViewModel() {
            this.ByteCodeEditor = new BytecodeEditorViewModel(this);
            this.ExceptionEditor = new ExceptionTableViewModel(this);
        }

        public void Load(MethodNode node) {
            this.Method = node;
            this.ByteCodeEditor.Load(node);
            this.ExceptionEditor.Load(node);
        }

        public void Save(MethodNode node) {
            this.ByteCodeEditor.Save(node);
            this.ExceptionEditor.Save(node);
        }
    }
}
