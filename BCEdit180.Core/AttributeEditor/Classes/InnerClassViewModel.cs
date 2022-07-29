using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.AttributeEditor.Classes {
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
            set => RaisePropertyChanged(ref this.innerName, value);
        }

        private ClassAccessModifiers classAccess;
        public ClassAccessModifiers ClassAccess {
            get => this.classAccess;
            set => RaisePropertyChanged(ref this.classAccess, value);
        }

        public ICommand EditAccessCommand { get; }

        public InnerClassViewModel(ClassAttributeEditorViewModel clazz) {
            this.Class = clazz;
            this.EditAccessCommand = new RelayCommand(EditAccess);
        }

        public InnerClassViewModel(ClassAttributeEditorViewModel clazz, InnerClass inner) : this(clazz) {
            Load(inner);
        }

        public void EditAccess() {
            if (Dialog.AccessEditor.EditClassAccess(this.ClassAccess, out ClassAccessModifiers access).Result) {
                this.ClassAccess = access;
            }
        }

        public void Load(InnerClass node) {
            this.OuterClassName = node.OuterClassName?.Name ?? ""; // can be null in some cases cases
            this.InnerClassName = node.InnerClassName?.Name ?? "";
            this.InnerName = node.InnerName;
            this.ClassAccess = node.Access;
        }

        public void Save(InnerClass node) {
            // default to null inner/outer class
            node.InnerClassName = string.IsNullOrWhiteSpace(this.InnerClassName) ? null : new ClassName(this.InnerClassName);
            node.OuterClassName = string.IsNullOrWhiteSpace(this.OuterClassName) ? null : new ClassName(this.OuterClassName);
            node.InnerName = this.InnerName;
            node.Access = this.ClassAccess;
        }
    }
}