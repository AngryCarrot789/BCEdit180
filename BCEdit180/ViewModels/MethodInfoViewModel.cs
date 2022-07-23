using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Annotations;
using BCEdit180.CodeEditing;
using BCEdit180.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using JavaAsm.Instructions;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodInfoViewModel : BaseViewModel {
        private ClassNode owner;
        private MethodAccessModifiers access;       // done
        private string methodName;                  // done
        private MethodDescriptor descriptor;        // done
        private List<AttributeNode> attributes;     // bruh what
        private string signature;                   // done
        private ushort maxStack;                    // done
        private ushort maxLocals;                   // done
        private List<TryCatchNode> tryCatches;
        private InstructionList instructions;
        private List<AttributeNode> codeAttributes;
        private bool isDeprecated;                  // done
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

        public List<AttributeNode> CodeAttributes {
            get => this.codeAttributes;
            set => RaisePropertyChanged(ref this.codeAttributes, value);
        }

        public AnnotationEditorViewModel VisibleAnnotationEditor { get; }

        public AnnotationEditorViewModel InvisibleAnnotationEditor { get; }

        public CodeEditorViewModel CodeEditor { get; }

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

        public ICommand EditDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public MethodNode Node { get; private set; }

        public MethodInfoViewModel(MethodNode node) {
            this.EditAccessCommand = new RelayCommand(() => ViewManager.ShowAccessEditor(this));
            this.VisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.InvisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.CodeEditor = new CodeEditorViewModel(node);
            Load(node);
        }

        public void Load(MethodNode node) {
            this.Node = node;
            this.MethodName = node.Name;
            this.Access = node.Access;
            this.MethodName = node.Name;
            this.Descriptor = node.Descriptor;
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
            this.Throws = node.Throws;
            this.AnnotationDefaultValue = node.AnnotationDefaultValue;
            this.CodeEditor.Load(node);
        }

        public void Save(MethodNode node) {
            node.Name = this.MethodName;
            node.Access = this.Access;
            node.Name = this.MethodName;
            node.Descriptor = this.Descriptor;
            node.Attributes = this.Attributes;
            node.Signature = this.Signature;
            node.MaxStack = this.MaxStack;
            node.MaxLocals = this.MaxLocals;
            node.CodeAttributes = this.CodeAttributes;
            node.VisibleAnnotations = new List<AnnotationNode>(this.VisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.InvisibleAnnotations = new List<AnnotationNode>(this.InvisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.IsDeprecated = this.IsDeprecated;
            node.Throws = this.Throws;
            node.AnnotationDefaultValue = this.AnnotationDefaultValue;
            this.CodeEditor.Save(node);
        }
    }
}