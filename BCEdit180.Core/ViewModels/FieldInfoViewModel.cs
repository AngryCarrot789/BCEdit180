using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Core.Annotations;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages.ErrorReporting;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    public class FieldInfoViewModel : BaseViewModel, IFieldDescriptable {
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

        private TypeDescriptor fieldDescriptor;
        public TypeDescriptor FieldDescriptor {
            get => this.fieldDescriptor;
            set {
                RaisePropertyChanged(ref this.fieldDescriptor, value);
                MessageDispatcher.Publish(new CheckField(this));
            }
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
            set {
                RaisePropertyChanged(ref this.constantValue, value);
                MessageDispatcher.Publish(new CheckField(this));
            }
        }

        private bool isCreatedRuntime;
        public bool IsCreatedRuntime {
            get => this.isCreatedRuntime;
            set => RaisePropertyChanged(ref this.isCreatedRuntime, value);
        }

        public AnnotationEditorViewModel VisibleAnnotationEditor { get; }

        public AnnotationEditorViewModel InvisibleAnnotationEditor { get; }

        public ICommand EditFieldDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public ICommand EditConstValueCommand { get; }

        public ICommand SetConstValueNullCommand { get; }

        public FieldNode Node { get; private set; }

        public FieldListViewModel FieldList { get; }

        public FieldInfoViewModel(FieldListViewModel list, FieldNode node) {
            this.FieldList = list;
            this.Node = node;
            this.EditAccessCommand = new RelayCommand(EditAccessAction);
            this.EditFieldDescriptorCommand = new RelayCommand(EditDescriptorAction);
            this.VisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.InvisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.EditConstValueCommand = new RelayCommand(EditConstValueAction);
            this.SetConstValueNullCommand = new RelayCommand(() => this.ConstantValue = null);
            Load(node);
        }

        public void EditAccessAction() {
            if (DialogUtils.ShowFieldAcccessDialog(this.Access, out FieldAccessModifiers modifiers)) {
                this.Access = modifiers;
            }
        }

        public void EditDescriptorAction() {
            if (DialogUtils.EditType(this.FieldDescriptor, out TypeDescriptor descriptor)) {
                this.FieldDescriptor = descriptor;
            }
        }

        public void EditConstValueAction() {
            ConstValueEditorViewModel vm = new ConstValueEditorViewModel(this.ConstantValue);
            vm.IsEnabledMethodDescriptor = false;
            vm.IsEnabledHandle = false;
            vm.IsEnabledClass = false;

            if (DialogUtils.EditConstantDialog(vm, out ConstValueEditorViewModel editor)) {
                if (editor.CheckEnabledStatesWithDialog()) {
                    if (editor.TryGetValue(out object value, out string error)) {
                        this.ConstantValue = value;
                    }
                    else if (error != null) {
                        Dialogs.Message.ShowMessage("Invalid value", error);
                    }
                }
            }
        }

        public void Load(FieldNode node) {
            this.Node = node;
            this.FieldName = node.Name;
            this.Access = node.Access;
            this.FieldDescriptor = node.Descriptor;
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
            node.Descriptor = this.FieldDescriptor;
            node.Attributes = this.Attributes;
            node.Signature = this.Signature;
            node.VisibleAnnotations = new List<AnnotationNode>(this.VisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.InvisibleAnnotations = new List<AnnotationNode>(this.InvisibleAnnotationEditor.Annotations.Select(a => a.Node));
            node.IsDeprecated = this.IsDeprecated;
        }
    }
}
