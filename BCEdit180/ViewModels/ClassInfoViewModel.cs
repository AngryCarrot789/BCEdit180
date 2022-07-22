using System.IO;
using JavaAsm;
using JavaAsm.IO;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ClassInfoViewModel : BaseViewModel {
        private int minorVersion;
        private ClassVersion majorVersion;
        private ClassAccessModifiers accessFlags;
        private string className;
        private string superName;
        private int interfaceCount;
        private int fieldCount;
        private int methodCount;
        private int attributeCount;

        private ClassNode classNode;

        public int MinorVersion {
            get => this.minorVersion;
            set => RaisePropertyChanged(ref this.minorVersion, value);
        }

        public ClassVersion MajorVersion {
            get => this.majorVersion;
            set => RaisePropertyChanged(ref this.majorVersion, value);
        }

        public ClassAccessModifiers AccessFlags {
            get => this.accessFlags;
            set => RaisePropertyChanged(ref this.accessFlags, value);
        }

        public string ClassName {
            get => this.className;
            set => RaisePropertyChanged(ref this.className, value);
        }

        public string SuperName {
            get => this.superName;
            set => RaisePropertyChanged(ref this.superName, value);
        }

        public int InterfaceCount {
            get => this.interfaceCount;
            set => RaisePropertyChanged(ref this.interfaceCount, value);
        }

        public int FieldCount {
            get => this.fieldCount;
            set => RaisePropertyChanged(ref this.fieldCount, value);
        }

        public int MethodCount {
            get => this.methodCount;
            set => RaisePropertyChanged(ref this.methodCount, value);
        }

        public int AttributeCount {
            get => this.attributeCount;
            set => RaisePropertyChanged(ref this.attributeCount, value);
        }

        public void Load(ClassNode node) {
            this.classNode = node;
            this.MinorVersion = node.MinorVersion;
            this.MajorVersion = node.MajorVersion;
            this.AccessFlags = node.Access;
            this.ClassName = node.Name.Name;
            this.SuperName = node.SuperName.Name;
            this.InterfaceCount = node.Interfaces.Count;
            this.FieldCount = node.Fields.Count;
            this.MethodCount = node.Methods.Count;
            this.AttributeCount = node.Attributes.Count;
        }

        public static ClassInfoViewModel FromFile(string filePath) {
            using (BufferedStream stream = new BufferedStream(File.OpenRead(filePath))) {
                ClassNode node = ClassFile.ParseClass(stream);
                ClassInfoViewModel vm = new ClassInfoViewModel();
                vm.Load(node);
                return vm;
            }
        }
    }
}
