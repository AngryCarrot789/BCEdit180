using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BCEdit180.Core.Annotations.Entries;
using BCEdit180.Core.Utils;
using JavaAsm;
using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Annotations {
    public class AnnotationViewModel : BaseViewModel {
        private readonly AnnotationNode node;

        public AnnotationNode Node { get => this.node; }

        private string previewTypeName;
        public string PreviewTypeName { // should not be edited by users; only used as a preview
            get => this.previewTypeName;
            set => RaisePropertyChanged(ref this.previewTypeName, value);
        }

        private string fullTypeName;
        public string FullTypeName {
            get => this.fullTypeName;
            set {
                RaisePropertyChanged(ref this.fullTypeName, value);
                UpdatePreviewName();
            }
        }

        private int arrayDepth;
        public int ArrayDepth { // read only
            get => this.arrayDepth;
            set => RaisePropertyChanged(ref this.arrayDepth, value);
        }

        private int sizeOnStack;
        public int SizeOnStack { // read only
            get => this.sizeOnStack;
            set => RaisePropertyChanged(ref this.sizeOnStack, value);
        }

        public ObservableCollection<BaseAnnotationEntryViewModel> Entries { get; }

        public AnnotationViewModel(AnnotationNode node) {
            this.node = node;
            this.Entries = new ObservableCollection<BaseAnnotationEntryViewModel>();
            Load(node);
        }

        public void Load(AnnotationNode node) {
            this.FullTypeName = node.Type.ClassName.Name;
            UpdatePreviewName();
            this.Entries.Clear();
            foreach (BaseAnnotationEntryViewModel item in node.ElementValuePairs.Select(a => BaseAnnotationEntryViewModel.Of(this, a))) {
                if (item != null) {
                    this.Entries.Add(item);
                }
            }

            this.ArrayDepth = node.Type.ArrayDepth;
            this.SizeOnStack = node.Type.SizeOnStack;
        }

        public void Save(AnnotationNode node) {
            node.Type = TypeDescriptor.Parse(this.FullTypeName, false);
            node.ElementValuePairs = new List<AnnotationNode.ElementValuePair>(this.Entries.Select(a => a.SaveAndGet()));
        }

        private void UpdatePreviewName() {
            int index = this.FullTypeName.LastIndexOf('/');
            this.PreviewTypeName = index == -1 ? this.FullTypeName : this.FullTypeName.Substring(index + 1);
        }
    }
}
