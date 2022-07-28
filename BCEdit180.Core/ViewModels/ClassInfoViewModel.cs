using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class ClassInfoViewModel : BaseViewModel, ISaveable<ClassNode> {
        private int minorVersion;
        private ClassVersion majorVersion;
        private ClassAccessModifiers accessFlags;
        private string className;
        private string superName;
        private int attributeCount;

        public ClassViewModel Class { get; }

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

        public ObservableCollection<ReferenceObjectViewModel<string>> Interfaces { get; }

        private ReferenceObjectViewModel<string> selectedInterface;
        public ReferenceObjectViewModel<string> SelectedInterface {
            get => this.selectedInterface;
            set => RaisePropertyChanged(ref this.selectedInterface, value);
        }

        public int AttributeCount {
            get => this.attributeCount;
            set => RaisePropertyChanged(ref this.attributeCount, value);
        }

        public ICommand EditAccessCommand { get; }
        public ICommand AddInterfaceCommand { get; }
        public ICommand RemoveInterfaceCommand { get; }

        public ClassInfoViewModel(ClassViewModel classVM) {
            this.Class = classVM;
            this.Interfaces = new ObservableCollection<ReferenceObjectViewModel<string>>();
            this.EditAccessCommand = new RelayCommand(EditAccess);
            this.AddInterfaceCommand = new RelayCommand(AddInterfaceAction);
            this.RemoveInterfaceCommand = new RelayCommand(RemoveSelectedInterfaceAction);
        }

        public void AddInterfaceAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(out TypeDescriptor desc, true, false).Result) {
                if (desc.ClassName != null && desc.ClassName.Name != null) {
                    this.Interfaces.Add(new ReferenceObjectViewModel<string>(desc.ClassName.Name));
                }
            }
        }

        public void RemoveSelectedInterfaceAction() {
            if (this.SelectedInterface != null) {
                this.Interfaces.Remove(this.SelectedInterface);
            }
        }

        public void RemoveInterfaceAction() {

        }

        public void EditAccess() {
            if (Dialog.AccessEditor.EditClassAccess(this.AccessFlags | ClassAccessModifiers.Super, out ClassAccessModifiers access).Result) {
                this.AccessFlags = access;
            }
        }

        public void Load(ClassNode node) {
            this.MinorVersion = node.MinorVersion;
            this.MajorVersion = node.MajorVersion;
            this.AccessFlags = node.Access;
            this.ClassName = node.Name.Name;
            this.SuperName = node.SuperName.Name;
            this.Interfaces.Clear();
            this.Interfaces.AddAll(node.Interfaces.Select(c => new ReferenceObjectViewModel<string>(c.Name)));
            this.AttributeCount = node.Attributes.Count;
        }

        public void Save(ClassNode node) {
            node.MinorVersion = (ushort) this.MinorVersion;
            node.MajorVersion = this.MajorVersion;
            node.Access = this.AccessFlags;
            node.Name = new ClassName(this.ClassName);
            node.SuperName = new ClassName(this.SuperName);
            node.Interfaces = new List<ClassName>(this.Interfaces.Select(c => new ClassName(c.Value)));
        }
    }
}
