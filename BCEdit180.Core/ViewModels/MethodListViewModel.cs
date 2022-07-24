using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core;
using BCEdit180.Core.Dialogs;
using BCEdit180.Core.Editors;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodListViewModel : BaseViewModel, ISaveable<ClassNode> {
        public ObservableCollection<MethodInfoViewModel> RemovedMethods { get; }
        public ObservableCollection<MethodInfoViewModel> Methods { get; }

        private MethodInfoViewModel selectedMethod;
        public MethodInfoViewModel SelectedMethod {
            get => this.selectedMethod;
            set => RaisePropertyChanged(ref this.selectedMethod, value);
        }

        public ICommand CreateMethodCommand { get; }

        public ClassViewModel Class { get; }

        public MethodListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Methods = new ObservableCollection<MethodInfoViewModel>();
            this.RemovedMethods = new ObservableCollection<MethodInfoViewModel>();
            this.CreateMethodCommand = new RelayCommand(ShowCreateMethodDialog);
        }

        public async void ShowCreateMethodDialog() {
            CreateMethod(await Dialog.TypeEditor.EditMethodDialog(true, null));
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
        }

        public void Save(ClassNode node) {
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
    }
}
