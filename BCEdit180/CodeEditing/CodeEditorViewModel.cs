using BCEdit180.CodeEditing.Bytecode.Locals;
using BCEdit180.ViewModels;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing {
    public class CodeEditorViewModel : BaseViewModel {
        public BytecodeEditorViewModel ByteCodeEditor { get; }
        public ExceptionTableViewModel ExceptionEditor { get; }
        public LocalVariableTableViewModel LocalVariableTable { get; }

        public MethodInfoViewModel MethodInfo { get; }

        public CodeEditorViewModel(MethodInfoViewModel methodInfo) {
            this.MethodInfo = methodInfo;
            this.ByteCodeEditor = new BytecodeEditorViewModel(this);
            this.ExceptionEditor = new ExceptionTableViewModel(this);
            this.LocalVariableTable = new LocalVariableTableViewModel(this);
        }

        public void Load(MethodNode node) {
            this.ByteCodeEditor.Load(node);
            this.ExceptionEditor.Load(node);
            this.LocalVariableTable.Load(node);
        }

        public void Save(MethodNode node) {
            this.ByteCodeEditor.Save(node);
            this.ExceptionEditor.Save(node);
            this.LocalVariableTable.Save(node);
        }
    }
}
