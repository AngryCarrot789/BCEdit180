using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode.Locals {
    public class LocalVariableTableViewModel : BaseViewModel {
        public CodeEditorViewModel CodeEditor { get; }

        public ObservableCollection<LocalVariableViewModel> LocalVariables { get; }

        private LocalVariableViewModel selectedEntry;
        public LocalVariableViewModel SelectedEntry {
            get => this.selectedEntry;
            set => RaisePropertyChanged(ref this.selectedEntry, value);
        }

        public ICommand AddLVTEntryCommand { get; }
        public ICommand RemoveSelectedEntryCommand { get; }
        public ICommand ClearLVTCommand { get; }

        public LocalVariableTableViewModel(CodeEditorViewModel codeEditor) {
            this.CodeEditor = codeEditor;
            this.LocalVariables = new ObservableCollection<LocalVariableViewModel>();
            this.AddLVTEntryCommand = new RelayCommand(() => this.LocalVariables.Add(new LocalVariableViewModel() {
                Descriptor =new TypeDescriptor(PrimitiveType.Integer, 0),
                StartPC = 0,
                Length = 0,
                Index = (ushort) this.LocalVariables.Count,
                VariableName = "myVariableName"
            }));

            this.RemoveSelectedEntryCommand = new RelayCommand(() => this.LocalVariables.Remove(this.SelectedEntry));
            this.ClearLVTCommand = new RelayCommand(() => this.LocalVariables.Clear());
        }

        public void Load(MethodNode node) {
            this.LocalVariables.Clear();
            this.LocalVariables.AddAll(node.LocalVariableTable.Select(t => new LocalVariableViewModel() {
                Index = t.Index,
                StartPC = t.StartPc,
                Length = t.Length,
                VariableName = t.Name,
                Descriptor = t.Descriptor,
            }));
        }

        public void Save(MethodNode node) {
            // I haven't looked through the code of java-asm,
            // but i'm fairly certain these labels are calculated when writing to a file
            // so editing these isn't necessary, and also isn't possible without reflection :/

            // It will let you edit the handler type, so that's a +
            // not sure how useful that is though but meh
            node.LocalVariableTable = new List<LocalVariableTableAttribute.LocalVariableTableEntry>();
            foreach (LocalVariableViewModel vm in this.LocalVariables) {
                node.LocalVariableTable.Add(new LocalVariableTableAttribute.LocalVariableTableEntry() {
                    Index = vm.Index,
                    StartPc = vm.StartPC,
                    Length = vm.Length,
                    Name = vm.VariableName,
                    Descriptor = vm.Descriptor,
                });
            }

            node.LocalVariableTable = node.LocalVariableTable.ToList();
        }
    }
}
