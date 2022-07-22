using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BCEdit180.Utils;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing {
    public class ExceptionTableViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }

        public ObservableCollection<TryCatchBlockViewModel> TryCatchBlocks { get; }

        public ExceptionTableViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.TryCatchBlocks = new ObservableCollection<TryCatchBlockViewModel>();
        }

        public void Load(MethodNode node) {
            this.TryCatchBlocks.Clear();
            this.TryCatchBlocks.AddAll(node.TryCatches.Select(t => new TryCatchBlockViewModel(t)));
        }

        public void Save(MethodNode node) {
            // I haven't looked through the code of java-asm,
            // but i'm fairly certain these labels are calculated when writing to a file
            // so editing these isn't necessary, and also isn't possible without reflection :/

            // It will let you edit the handler type, so that's a +
            // not sure how useful that is though but meh
            node.TryCatches = new List<TryCatchNode>(this.TryCatchBlocks.Select(t => t.SaveAndGetNode()));
        }
    }
}
