using System.Collections.Generic;
using System.Windows.Input;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using JavaAsm.Instructions;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodInfoViewModel : BaseViewModel {
        private MethodNode method;

        private ClassNode owner;
        private MethodAccessModifiers access;
        private string methodName;
        private MethodDescriptor descriptor;
        private List<AttributeNode> attributes;
        private string signature;
        private ushort maxStack;
        private ushort maxLocals;
        private List<TryCatchNode> tryCatches;
        private InstructionList instructions;
        private List<AttributeNode> codeAttributes;
        private List<AnnotationNode> invisibleAnnotations;
        private List<AnnotationNode> visibleAnnotations;
        private bool isDeprecated;
        private List<ClassName> throws;
        private ElementValue annotationDefaultValue;

        public ClassNode Owner {
            get => this.owner;
            set => RaisePropertyChanged(ref this.owner, value);
        }

        public MethodAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        public string MethodName {
            get => this.methodName;
            set => RaisePropertyChanged(ref this.methodName, value);
        }

        public MethodDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public List<AttributeNode> Attributes {
            get => this.attributes;
            set => RaisePropertyChanged(ref this.attributes, value);
        }

        public string Signature {
            get => this.signature;
            set => RaisePropertyChanged(ref this.signature, value);
        }

        public ushort MaxStack {
            get => this.maxStack;
            set => RaisePropertyChanged(ref this.maxStack, value);
        }

        public ushort MaxLocals {
            get => this.maxLocals;
            set => RaisePropertyChanged(ref this.maxLocals, value);
        }

        public List<TryCatchNode> TryCatches {
            get => this.tryCatches;
            set => RaisePropertyChanged(ref this.tryCatches, value);
        }

        public InstructionList Instructions {
            get => this.instructions;
            set => RaisePropertyChanged(ref this.instructions, value);
        }

        public List<AttributeNode> CodeAttributes {
            get => this.codeAttributes;
            set => RaisePropertyChanged(ref this.codeAttributes, value);
        }

        public List<AnnotationNode> InvisibleAnnotations {
            get => this.invisibleAnnotations;
            set => RaisePropertyChanged(ref this.invisibleAnnotations, value);
        }

        public List<AnnotationNode> VisibleAnnotations {
            get => this.visibleAnnotations;
            set => RaisePropertyChanged(ref this.visibleAnnotations, value);
        }

        public bool IsDeprecated {
            get => this.isDeprecated;
            set => RaisePropertyChanged(ref this.isDeprecated, value);
        }

        public List<ClassName> Throws {
            get => this.throws;
            set => RaisePropertyChanged(ref this.throws, value);
        }

        public ElementValue AnnotationDefaultValue {
            get => this.annotationDefaultValue;
            set => RaisePropertyChanged(ref this.annotationDefaultValue, value);
        }

        public ICommand EditSignatureCommand { get; }

        public ICommand EditAccessCommand { get; }

        public MethodInfoViewModel(MethodNode method) {
            this.method = method;
            Load(method);
        }

        public void Load(MethodNode node) {
            this.MethodName = node.Name;
        }

        public void Save(MethodNode node) {
            node.Name = this.MethodName;
        }
    }
}