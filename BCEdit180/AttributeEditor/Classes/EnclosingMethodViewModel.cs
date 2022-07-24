using System.Windows.Input;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.AttributeEditor.Classes {
    public class EnclosingMethodViewModel : BaseViewModel, ISaveable<ClassNode> {
        private string className;
        public string ClassName {
            get => this.className;
            set => RaisePropertyChanged(ref this.className, value);
        }

        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private MethodDescriptor descriptor;

        public MethodDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public ICommand EditDescriptorCommand { get; }

        public EnclosingMethodViewModel() {
            this.EditDescriptorCommand = new RelayCommand(() => {
                ViewManager.ShowEditMethodDesc((m) => this.Descriptor = m, this.Descriptor?.ReturnType, this.Descriptor?.ArgumentTypes);
            });
        }

        public void Load(ClassNode node) {
            if (node.EnclosingMethod != null) {
                this.ClassName = node.EnclosingMethod.Class?.Name;
                this.MethodName = node.EnclosingMethod.MethodName;
                this.Descriptor = node.EnclosingMethod.MethodDescriptor;
            }
            else {
                this.ClassName = null;
                this.MethodName = null;
                this.Descriptor = null;
            }
        }

        public void Save(ClassNode node) {
            if (node.EnclosingMethod == null) {
                node.EnclosingMethod = new EnclosingMethodAttribute();
            }

            node.EnclosingMethod.Class = new ClassName(this.ClassName);
            node.EnclosingMethod.MethodName = this.MethodName;
            node.EnclosingMethod.MethodDescriptor = this.Descriptor;
        }
    }
}