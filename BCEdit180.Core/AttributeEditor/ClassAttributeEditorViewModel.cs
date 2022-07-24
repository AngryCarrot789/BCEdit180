using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.AttributeEditor.Classes;
using BCEdit180.Core.ViewModels;
using JavaAsm;
using JavaAsm.CustomAttributes;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.AttributeEditor {
    public class ClassAttributeEditorViewModel : BaseViewModel, ISaveable<ClassNode> {
        public ClassViewModel Class { get; }

        public SourceFileViewModel SourceFile { get; }

        public SourceDebugExtensionViewModel SourceDebug { get; }

        public ObservableCollection<InnerClassViewModel> InnerClasses { get; }

        public EnclosingMethodViewModel EnclosingMethod { get; }

        public ObservableCollection<BootstrapMethodViewModel> BootstrapMethods { get; }

        private bool isEnabledSourceFile;
        public bool IsEnabledSourceFile {
            get => this.isEnabledSourceFile;
            set => RaisePropertyChanged(ref this.isEnabledSourceFile, value);
        }

        private bool isEnabledSourceDebug;
        public bool IsEnabledSourceDebug {
            get => this.isEnabledSourceDebug;
            set => RaisePropertyChanged(ref this.isEnabledSourceDebug, value);
        }

        private bool isEnabledInnerClasses;
        public bool IsEnabledInnerClasses {
            get => this.isEnabledInnerClasses;
            set => RaisePropertyChanged(ref this.isEnabledInnerClasses, value);
        }

        private bool isEnabledEnclosingMethod;
        public bool IsEnabledEnclosingMethod {
            get => this.isEnabledEnclosingMethod;
            set => RaisePropertyChanged(ref this.isEnabledEnclosingMethod, value);
        }

        private bool isEnabledBootstrapMethods;
        public bool IsEnabledBootstrapMethods {
            get => this.isEnabledBootstrapMethods;
            set => RaisePropertyChanged(ref this.isEnabledBootstrapMethods, value);
        }

        public ClassAttributeEditorViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.SourceFile = new SourceFileViewModel();
            this.SourceDebug = new SourceDebugExtensionViewModel();
            this.InnerClasses = new ObservableCollection<InnerClassViewModel>();
            this.EnclosingMethod = new EnclosingMethodViewModel();
            this.BootstrapMethods = new ObservableCollection<BootstrapMethodViewModel>();
        }

        public void Load(ClassNode node) {
            this.InnerClasses.Clear();
            this.BootstrapMethods.Clear();

            if (string.IsNullOrEmpty(node.SourceFile)) {
                this.isEnabledSourceFile = false;
                this.SourceFile.SourceFile = null;
            }
            else {
                this.isEnabledSourceFile = true;
                this.SourceFile.SourceFile = node.SourceFile;
            }

            if (string.IsNullOrEmpty(node.SourceDebugExtension)) {
                this.IsEnabledSourceDebug = false;
                this.SourceDebug.Value = null;
            }
            else {
                this.IsEnabledSourceDebug = true;
                this.SourceDebug.Value = node.SourceDebugExtension;
            }

            if (node.InnerClasses != null && node.InnerClasses.Count > 0) {
                this.IsEnabledInnerClasses = true;
                foreach (InnerClass innerClass in node.InnerClasses) {
                    this.InnerClasses.Add(new InnerClassViewModel(this, innerClass));
                }
            }

            this.EnclosingMethod.Load(node);
            this.IsEnabledEnclosingMethod = node.EnclosingMethod != null;

            // TODO: load bootstrap methods, if it's even possible
        }

        public void Save(ClassNode node) {
            node.SourceFile = this.IsEnabledSourceFile ? this.SourceFile.SourceFile : null;
            node.SourceDebugExtension = this.IsEnabledSourceDebug ? this.SourceDebug.Value : null;
            if (this.IsEnabledInnerClasses) {
                List<InnerClass> classes = new List<InnerClass>();
                foreach (InnerClassViewModel clazz in this.InnerClasses) {
                    InnerClass inner = new InnerClass();
                    clazz.Save(inner);
                    classes.Add(inner);
                }

                node.InnerClasses = classes;
            }
            else {
                node.InnerClasses = new List<InnerClass>();
            }

            if (this.IsEnabledEnclosingMethod) {
                this.EnclosingMethod.Save(node);
            }
            else {
                node.EnclosingMethod = null;
            }

            // TODO: Bootstrap methods
        }
    }
}