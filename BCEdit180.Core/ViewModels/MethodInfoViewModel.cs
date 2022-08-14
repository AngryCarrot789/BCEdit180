using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Annotations;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class MethodInfoViewModel : BaseViewModel, IDisposable, ISaveable<MethodNode>, IMethodDescriptable {
        private MethodAccessModifiers access;
        public MethodAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        private string methodName;
        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        private MethodDescriptor methodDescriptor;
        public MethodDescriptor MethodDescriptor {
            get => this.methodDescriptor;
            set => RaisePropertyChanged(ref this.methodDescriptor, value);
        }

        private List<AttributeNode> attributes;
        public List<AttributeNode> Attributes {
            get => this.attributes;
            set => RaisePropertyChanged(ref this.attributes, value);
        }

        private string signature;
        public string Signature {
            get => this.signature;
            set => RaisePropertyChanged(ref this.signature, value);
        }

        private ushort maxStack;
        public ushort MaxStack {
            get => this.maxStack;
            set => RaisePropertyChanged(ref this.maxStack, value);
        }

        private ushort maxLocals;
        public ushort MaxLocals {
            get => this.maxLocals;
            set => RaisePropertyChanged(ref this.maxLocals, value);
        }

        private List<AttributeNode> codeAttributes;
        public List<AttributeNode> CodeAttributes {
            get => this.codeAttributes;
            set => RaisePropertyChanged(ref this.codeAttributes, value);
        }

        private bool isDeprecated;
        public bool IsDeprecated {
            get => this.isDeprecated;
            set => RaisePropertyChanged(ref this.isDeprecated, value);
        }

        public ObservableCollection<ReferenceObjectViewModel<string>> Throws { get; }

        private ElementValue annotationDefaultValue;
        public ElementValue AnnotationDefaultValue {
            get => this.annotationDefaultValue;
            set => RaisePropertyChanged(ref this.annotationDefaultValue, value);
        }

        private bool isCreatedRuntime;
        public bool IsCreatedRuntime {
            get => this.isCreatedRuntime;
            set => RaisePropertyChanged(ref this.isCreatedRuntime, value);
        }

        public AnnotationEditorViewModel VisibleAnnotationEditor { get; }

        public AnnotationEditorViewModel InvisibleAnnotationEditor { get; }

        public CodeEditorViewModel CodeEditor { get; }

        public ICommand EditMethodDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public ICommand CalculateMaxStackCommand { get; }

        public ICommand CalculateMaxLocalsCommand { get; }

        public MethodListViewModel MethodList { get; }

        public MethodNode Node { get; private set; }

        public ClassViewModel Class => this.MethodList.Class;

        public MethodInfoViewModel(MethodListViewModel methodList, MethodNode node) {
            this.MethodList = methodList;
            this.Node = node;
            this.Throws = new ObservableCollection<ReferenceObjectViewModel<string>>();
            this.CalculateMaxStackCommand = new RelayCommand(CalculateMaxStackSize);
            this.CalculateMaxLocalsCommand = new RelayCommand(CalculateMaxLocalsSize);
            this.EditAccessCommand = new RelayCommand(EditAccess);
            this.EditMethodDescriptorCommand = new RelayCommand(EditDescriptor);
            this.VisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.InvisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.CodeEditor = new CodeEditorViewModel(this);
            Load(node);
        }

        public void EditAccess() {
            if (Dialog.AccessEditor.EditMethodAccess(this.Access, out MethodAccessModifiers access).Result) {
                this.Access = access;
            }
        }

        public void EditDescriptor() {
            if (Dialog.TypeEditor.EditMethodDescriptorDialog(this.MethodDescriptor, out MethodDescriptor typeDesc).Result) {
                this.MethodDescriptor = typeDesc;
            }
        }

        public void CalculateMaxStackSize() {

        }

        public void CalculateMaxLocalsSize() {

        }

        public void Load(MethodNode node) {
            this.Node = node;
            this.MethodName = node.Name;
            this.Access = node.Access;
            this.MethodName = node.Name;
            this.MethodDescriptor = node.Descriptor;
            this.Attributes = node.Attributes;
            this.Signature = node.Signature;
            this.MaxStack = node.MaxStack;
            this.MaxLocals = node.MaxLocals;
            this.CodeAttributes = node.CodeAttributes;
            this.VisibleAnnotationEditor.Annotations.Clear();
            this.VisibleAnnotationEditor.Annotations.AddAll(node.VisibleAnnotations.Select(a => new AnnotationViewModel(a)));
            this.InvisibleAnnotationEditor.Annotations.Clear();
            this.InvisibleAnnotationEditor.Annotations.AddAll(node.InvisibleAnnotations.Select(a => new AnnotationViewModel(a)));
            this.IsDeprecated = node.IsDeprecated;
            this.Throws.Clear();
            this.Throws.AddAll(node.Throws.Select(a => new ReferenceObjectViewModel<string>(a.Name)));
            this.AnnotationDefaultValue = node.AnnotationDefaultValue;
            this.CodeEditor.Load(node);
        }

        public void Save(MethodNode node) {
            node.Name = this.MethodName;
            node.Access = this.Access;
            node.Name = this.MethodName;
            node.Descriptor = this.MethodDescriptor;
            node.Attributes = this.Attributes;
            node.Signature = this.Signature;
            node.MaxStack = this.MaxStack;
            node.MaxLocals = this.MaxLocals;
            node.CodeAttributes = this.CodeAttributes;
            node.VisibleAnnotations = new List<AnnotationNode>(this.VisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.InvisibleAnnotations = new List<AnnotationNode>(this.InvisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.IsDeprecated = this.IsDeprecated;
            node.Throws = this.Throws.Select(a => new ClassName(a.Value)).ToList();
            node.AnnotationDefaultValue = this.AnnotationDefaultValue;
            this.CodeEditor.Save(node);
        }

        public void Dispose() {
            this.CodeEditor.Dispose();
        }
    }
}