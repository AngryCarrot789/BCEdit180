using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class MethodListViewModel : BaseViewModel, IDisposable, ISaveable<ClassNode> {
        public static IListSelector<MethodInfoViewModel> MethodList { get; set; }

        public ObservableCollection<MethodInfoViewModel> RemovedMethods { get; }
        public ObservableCollection<MethodInfoViewModel> Methods { get; }

        private MethodInfoViewModel selectedMethod;
        public MethodInfoViewModel SelectedMethod {
            get => this.selectedMethod;
            set => RaisePropertyChanged(ref this.selectedMethod, value);
        }

        private int lastSaveIndex;
        private int selectedIndex;
        public int SelectedIndex {
            get => this.selectedIndex;
            set => RaisePropertyChanged(ref this.selectedIndex, value);
        }

        public SearchMethodNameViewModel SearchMethod { get; }

        public ICommand CreateMethodCommand { get; }

        public ClassViewModel Class { get; }

        public MethodListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Methods = new ObservableCollection<MethodInfoViewModel>();
            this.RemovedMethods = new ObservableCollection<MethodInfoViewModel>();
            this.CreateMethodCommand = new RelayCommand(ShowCreateMethodDialog);
            this.SearchMethod = new SearchMethodNameViewModel(this);
        }

        public void ShowCreateMethodDialog() {
            if (Dialog.TypeEditor.EditMethodDialog(out MethodEditorViewModel editor, true).Result) {
                CreateMethod(editor);
            }
        }

        public void CreateMethod(MethodEditorViewModel editor) {
            this.Methods.Add(new MethodInfoViewModel(this, CreateMethodNodeForVM(editor)) { IsCreatedRuntime = true });
        }

        private MethodNode CreateMethodNodeForVM(MethodEditorViewModel editor) {
            MethodNode method = new MethodNode() {
                Owner = this.Class.Node,
                Name = editor.MethodName,
                Descriptor = new MethodDescriptor(editor.ReturnType, new List<TypeDescriptor>(editor.Parameters.Select(x => x.Descriptor))),
                Access = editor.Access,
            };

            // non-static, so add 'this'
            if ((editor.Access & MethodAccessModifiers.Static) == 0) {
                method.MaxLocals = 1;
            }

            // waste of memory space, no need to name 'this'
            // if ((editor.Access & MethodAccessModifiers.Static) == 0) {
            //     node.LocalVariableNames[0] = new LocalVariableTableAttribute.LocalVariableTableEntry() {
            //         Descriptor = new TypeDescriptor(new ClassName(this.Class.ClassInfo.ClassName), 0),
            //         Name = "this",
            //         Index = 0
            //     };
            // }

            return method;
        }

        public void Load(ClassNode node) {
            this.RemovedMethods.Clear();
            this.Methods.Clear();
            foreach (MethodNode method in node.Methods) {
                this.Methods.Add(new MethodInfoViewModel(this, method));
            }

            if (this.lastSaveIndex >= 0 && this.lastSaveIndex < this.Methods.Count) {
                this.SelectedIndex = this.lastSaveIndex;
            }

            this.lastSaveIndex = 0;
        }

        public void Save(ClassNode node) {
            this.lastSaveIndex = this.SelectedIndex;
            foreach (MethodInfoViewModel removedMethod in this.RemovedMethods) {
                node.Methods.Remove(removedMethod.Node);
            }

            this.RemovedMethods.Clear();
            foreach (MethodInfoViewModel method in this.Methods) {
                if (method.IsCreatedRuntime) {
                    method.IsCreatedRuntime = false;
                    node.Methods.Add(method.Node);
                }

                method.Save(method.Node);
            }
        }

        public void Dispose() {
            this.SearchMethod.Dispose();
            foreach (MethodInfoViewModel method in this.Methods) {
                method.Dispose();
            }
        }
    }
}
