using System.Windows.Input;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.AttributeEditor.Classes {
    public class InnerClassViewModel : BaseViewModel, ISaveable<InnerClass> {
        public ClassAttributeEditorViewModel Class { get; }

        private string innerClassName;
        public string InnerClassName {
            get => this.innerClassName;
            set => RaisePropertyChanged(ref this.innerClassName, value);
        }

        private string outerClassName;
        public string OuterClassName {
            get => this.outerClassName;
            set => RaisePropertyChanged(ref this.outerClassName, value);
        }

        private string innerName;
        public string InnerName {
            get => this.innerName;
            set {
                RaisePropertyChanged(ref this.innerName, value);
            }
        }

        private ClassAccessModifiers classAccess;
        public ClassAccessModifiers ClassAccess {
            get => this.classAccess;
            set => RaisePropertyChanged(ref this.classAccess, value);
        }

        public ICommand EditAccessCommand { get; }

        public InnerClassViewModel(ClassAttributeEditorViewModel clazz) {
            this.Class = clazz;
            this.EditAccessCommand = new RelayCommand(() => {
                ViewManager.ShowAccessEditor(modifiers => this.ClassAccess = modifiers, this.ClassAccess);
            });
        }

        public InnerClassViewModel(ClassAttributeEditorViewModel clazz, InnerClass inner) : this(clazz) {
            Load(inner);
        }

        public void Load(InnerClass node) {
            this.InnerClassName = node.InnerClassName?.Name;
            this.OuterClassName = node.OuterClassName?.Name;
            this.InnerName = node.InnerName;
            this.ClassAccess = node.Access;
        }

        public void Save(InnerClass node) {
            node.InnerClassName = new ClassName(this.InnerClassName);
            node.OuterClassName = new ClassName(this.OuterClassName);
            node.InnerName = this.InnerName;
            node.Access = this.ClassAccess;
        }
    }
}