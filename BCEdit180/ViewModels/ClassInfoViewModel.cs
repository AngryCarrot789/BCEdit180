using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Input;
using BCEdit180.Utils;
using JavaAsm;
using JavaAsm.IO;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ClassInfoViewModel : BaseViewModel {
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

        public int AttributeCount {
            get => this.attributeCount;
            set => RaisePropertyChanged(ref this.attributeCount, value);
        }

        public ClassNode Node { get; private set; }

        public ICommand EditAccessCommand { get; }

        public ClassInfoViewModel(ClassViewModel classVM) {
            this.Class = classVM;
            this.Interfaces = new ObservableCollection<ReferenceObjectViewModel<string>>();
            this.EditAccessCommand = new RelayCommand(() => ViewManager.ShowAccessEditor(this));
        }

        public void Load(ClassNode node) {
            this.Node = node;
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
