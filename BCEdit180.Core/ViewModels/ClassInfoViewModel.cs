using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class ClassInfoViewModel : BaseViewModel, ISaveable<ClassNode> {
        public ClassViewModel Class { get; }

        private int minorVersion;
        public int MinorVersion {
            get => this.minorVersion;
            set => RaisePropertyChanged(ref this.minorVersion, value);
        }

        private ClassVersion majorVersion;
        public ClassVersion MajorVersion {
            get => this.majorVersion;
            set => RaisePropertyChanged(ref this.majorVersion, value);
        }

        private ClassAccessModifiers accessFlags;
        public ClassAccessModifiers AccessFlags {
            get => this.accessFlags;
            set => RaisePropertyChanged(ref this.accessFlags, value);
        }

        private string className;
        public string ClassName {
            get => this.className;
            set => RaisePropertyChanged(ref this.className, value);
        }

        private string superName;
        public string SuperName {
            get => this.superName;
            set => RaisePropertyChanged(ref this.superName, value);
        }

        private string signature;
        public string Signature {
            get => this.signature;
            set => RaisePropertyChanged(ref this.signature, value);
        }
        
        private string sourceDebugInfo;
        public string SourceDebugInfo {
            get => this.sourceDebugInfo;
            set => RaisePropertyChanged(ref this.sourceDebugInfo, value);
        }

        public ObservableCollection<ReferenceObjectViewModel<string>> Interfaces { get; }

        private ReferenceObjectViewModel<string> selectedInterface;
        public ReferenceObjectViewModel<string> SelectedInterface {
            get => this.selectedInterface;
            set => RaisePropertyChanged(ref this.selectedInterface, value);
        }

        public ICommand EditAccessCommand { get; }
        public ICommand AddInterfaceCommand { get; }
        public ICommand RemoveInterfaceCommand { get; }

        public RelayCommandParam<ReferenceObjectViewModel<string>> RemoveItemCommand { get; }

        public ClassInfoViewModel(ClassViewModel classVM) {
            this.Class = classVM;
            this.Interfaces = new ObservableCollection<ReferenceObjectViewModel<string>>();
            this.EditAccessCommand = new RelayCommand(EditAccess);
            this.AddInterfaceCommand = new RelayCommand(AddInterfaceAction);
            this.RemoveInterfaceCommand = new RelayCommand(RemoveSelectedInterfaceAction);
            this.RemoveItemCommand = new RelayCommandParam<ReferenceObjectViewModel<string>>(this.RemoveInterfaceAction);
        }

        public void RemoveInterfaceAction(ReferenceObjectViewModel<string> obj) {
            this.Interfaces.Remove(obj);
        }

        public void AddInterfaceAction() {
            if (DialogUtils.EditType(out TypeDescriptor desc, false, true)) {
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

        public void EditAccess() {
            if (DialogUtils.ShowClassAcccessDialog(this.AccessFlags | ClassAccessModifiers.Super, out ClassAccessModifiers modifiers)) {
                this.AccessFlags = modifiers;
            }
        }

        public void Load(ClassNode node) {
            this.MinorVersion = node.MinorVersion;
            this.MajorVersion = node.MajorVersion;
            this.AccessFlags = node.Access;
            this.ClassName = node.Name.Name;
            this.SuperName = node.SuperName.Name;
            this.Signature = node.Signature;
            this.SourceDebugInfo = node.SourceDebugExtension ?? "";
            this.Interfaces.Clear();
            this.Interfaces.AddAll(node.Interfaces.Select(c => new ReferenceObjectViewModel<string>(c.Name)));
        }

        public void Save(ClassNode node) {
            node.MinorVersion = (ushort) this.MinorVersion;
            node.MajorVersion = this.MajorVersion;
            node.Access = this.AccessFlags;
            node.Name = new ClassName(this.ClassName);
            node.SuperName = new ClassName(this.SuperName);
            node.Signature = this.Signature;
            node.SourceDebugExtension = string.IsNullOrWhiteSpace(this.SourceDebugInfo) ? null : this.SourceDebugInfo;
            node.Interfaces = new List<ClassName>(this.Interfaces.Select(c => new ClassName(c.Value)));
        }
    }
}
