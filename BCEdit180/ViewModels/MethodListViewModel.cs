using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using BCEdit180.MethodCreator;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodListViewModel : BaseViewModel {
        public ClassViewModel Class { get; }

        public ObservableCollection<MethodInfoViewModel> RemovedMethods { get; }
        public ObservableCollection<MethodInfoViewModel> AddedMethods { get; }
        public ObservableCollection<MethodInfoViewModel> Methods { get; }

        private MethodInfoViewModel selectedMethod;
        public MethodInfoViewModel SelectedMethod {
            get => this.selectedMethod;
            set => RaisePropertyChanged(ref this.selectedMethod, value);
        }

        public ICommand CreateMethodCommand { get; }

        public MethodListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Methods = new ObservableCollection<MethodInfoViewModel>();
            this.RemovedMethods = new ObservableCollection<MethodInfoViewModel>();
            this.AddedMethods = new ObservableCollection<MethodInfoViewModel>();
            this.CreateMethodCommand = new RelayCommand(ShowCreateMethodDialog);
        }

        public void ShowCreateMethodDialog() {
            ViewManager.ShowCreateMethod(CreateMethod);
        }

        public void CreateMethod(MethodEditorViewModel editor) {
            MethodNode node = new MethodNode() {
                Owner = this.Class.Node,
                Name = editor.MethodName,
                Descriptor = new MethodDescriptor(editor.ReturnType, new List<TypeDescriptor>(editor.Parameters.Select(x => x.Descriptor))),
                Access = editor.Access,
            };

            if (editor.Access != MethodAccessModifiers.Static) {
                node.LocalVariableNames[0] = new LocalVariableTableAttribute.LocalVariableTableEntry() {
                    Descriptor = new TypeDescriptor(new ClassName(this.Class.ClassInfo.ClassName), 0),
                    Index = 0
                };
            }

            MethodInfoViewModel method = new MethodInfoViewModel(this, node);
            method.Load(node);
            this.AddedMethods.Add(method);
        }

        public void Load(ClassNode clazz) {
            this.RemovedMethods.Clear();
            this.AddedMethods.Clear();
            this.Methods.Clear();
            foreach (MethodNode method in clazz.Methods) {
                this.Methods.Add(new MethodInfoViewModel(this, method));
            }
        }

        public void Save(ClassNode node) {
            List<MethodNode> removedNodes = this.RemovedMethods.Select(m => m.Node).ToList();
            this.RemovedMethods.Clear();
            node.Methods.RemoveAll(n => removedNodes.Contains(n));
            foreach (MethodInfoViewModel vm in this.AddedMethods) {
                node.Methods.Add(vm.Node);
            }

            this.AddedMethods.Clear();
            foreach (MethodInfoViewModel method in this.Methods) {
                method.Save(method.Node);
            }
        }
    }
}
