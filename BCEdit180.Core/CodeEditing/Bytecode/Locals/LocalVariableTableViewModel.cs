using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BCEdit180.Core.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode.Locals {
    public class LocalVariableTableViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }

        public ObservableCollection<LocalVariableViewModel> LocalVariables { get; }

        public LocalVariableTableViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.LocalVariables = new ObservableCollection<LocalVariableViewModel>();
        }

        public void Load(MethodNode node) {
            this.LocalVariables.Clear();
            this.LocalVariables.AddAll(node.LocalVariableNames.Select(t => new LocalVariableViewModel() {
                Index = (ushort) t.Key,
                StartPC = t.Value.StartPc,
                Length = t.Value.Length,
                VariableName = t.Value.Name,
                Descriptor = t.Value.Descriptor,
            }));
        }

        public void Save(MethodNode node) {
            // I haven't looked through the code of java-asm,
            // but i'm fairly certain these labels are calculated when writing to a file
            // so editing these isn't necessary, and also isn't possible without reflection :/

            // It will let you edit the handler type, so that's a +
            // not sure how useful that is though but meh
            node.LocalVariableNames = new Dictionary<int, LocalVariableTableAttribute.LocalVariableTableEntry>();
            foreach (LocalVariableViewModel vm in this.LocalVariables) {
                node.LocalVariableNames[vm.Index] = new LocalVariableTableAttribute.LocalVariableTableEntry() {
                    Index = vm.Index,
                    StartPc = vm.StartPC,
                    Length = vm.Length,
                    Name = vm.VariableName,
                    Descriptor = vm.Descriptor,
                };
            }
        }
    }
}
