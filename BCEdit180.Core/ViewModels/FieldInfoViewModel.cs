using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.Annotations;
using BCEdit180.Core.Dialogs;
using BCEdit180.Core.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class FieldInfoViewModel : BaseViewModel {
        private FieldAccessModifiers access;
        public FieldAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        private string fieldName;
        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        private TypeDescriptor descriptor;
        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
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

        private bool isDeprecated;
        public bool IsDeprecated {
            get => this.isDeprecated;
            set => RaisePropertyChanged(ref this.isDeprecated, value);
        }

        private object constantValue;
        public object ConstantValue {
            get => this.constantValue;
            set => RaisePropertyChanged(ref this.constantValue, value);
        }

        private bool isCreatedRuntime;
        public bool IsCreatedRuntime {
            get => this.isCreatedRuntime;
            set => RaisePropertyChanged(ref this.isCreatedRuntime, value);
        }

        public AnnotationEditorViewModel VisibleAnnotationEditor { get; }

        public AnnotationEditorViewModel InvisibleAnnotationEditor { get; }

        public ICommand EditDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public FieldNode Node { get; private set; }

        public FieldListViewModel FieldList { get; }

        public FieldInfoViewModel(FieldListViewModel list, FieldNode node) {
            this.FieldList = list;
            this.Node = node;
            this.EditAccessCommand = new RelayCommand(() => EditAccess());
            this.EditDescriptorCommand = new RelayCommand(() => EditDescriptor());
            this.VisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.InvisibleAnnotationEditor = new AnnotationEditorViewModel();
            Load(node);
        }

        public async Task EditAccess() {
            this.Access = await Dialog.AccessEditor.EditFieldAccess(this.Access);
        }

        public async Task EditDescriptor() {
            this.Descriptor = await Dialog.TypeEditor.EditTypeDescriptorDialog(this.Descriptor);
        }

        public void Load(FieldNode node) {
            this.Node = node;
            this.FieldName = node.Name;
            this.Access = node.Access;
            this.Descriptor = node.Descriptor;
            this.Attributes = node.Attributes;
            this.Signature = node.Signature;
            this.VisibleAnnotationEditor.Annotations.Clear();
            this.VisibleAnnotationEditor.Annotations.AddAll(node.VisibleAnnotations.Select(a => new AnnotationViewModel(a)));
            this.InvisibleAnnotationEditor.Annotations.Clear();
            this.InvisibleAnnotationEditor.Annotations.AddAll(node.InvisibleAnnotations.Select(a => new AnnotationViewModel(a)));
            this.IsDeprecated = node.IsDeprecated;
        }

        public void Save(FieldNode node) {
            node.Name = this.FieldName;
            node.Access = this.Access;
            node.Descriptor = this.Descriptor;
            node.Attributes = this.Attributes;
            node.Signature = this.Signature;
            node.VisibleAnnotations = new List<AnnotationNode>(this.VisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.InvisibleAnnotations = new List<AnnotationNode>(this.InvisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.IsDeprecated = this.IsDeprecated;
        }
    }
}
