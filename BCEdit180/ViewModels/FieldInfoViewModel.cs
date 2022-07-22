﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BCEdit180.Annotations;
using BCEdit180.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class FieldInfoViewModel : BaseViewModel {
        public FieldNode Node { get; }

        private FieldAccessModifiers access;
        private string fieldName;
        private TypeDescriptor descriptor;
        private List<AttributeNode> attributes;
        private string signature;
        private bool isDeprecated;
        private object constantValue;

        public FieldAccessModifiers Access {
            get => this.access;
            set => RaisePropertyChanged(ref this.access, value);
        }

        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        public TypeDescriptor Descriptor {
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

        public AnnotationEditorViewModel VisibleAnnotationEditor { get; }

        public AnnotationEditorViewModel InvisibleAnnotationEditor { get; }

        public bool IsDeprecated {
            get => this.isDeprecated;
            set => RaisePropertyChanged(ref this.isDeprecated, value);
        }

        public object ConstantValue {
            get => this.constantValue;
            set => RaisePropertyChanged(ref this.constantValue, value);
        }

        public ICommand EditDescriptorCommand { get; }

        public ICommand EditAccessCommand { get; }

        public FieldInfoViewModel(FieldNode node) {
            this.Node = node;
            this.EditAccessCommand = new RelayCommand(() => ViewManager.ShowAccessEditor(this));
            this.EditDescriptorCommand = new RelayCommand(() => ViewManager.ShowDescriptorEditor(this));
            this.VisibleAnnotationEditor = new AnnotationEditorViewModel();
            this.InvisibleAnnotationEditor = new AnnotationEditorViewModel();
            Load(node);
        }

        public void Load(FieldNode node) {
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
