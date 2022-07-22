using JavaAsm.CustomAttributes.Annotation;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Annotations {
    public class AnnotationViewModel : BaseViewModel {
        private readonly AnnotationNode node;

        public AnnotationNode Node { get => this.node; }

        private string typeName;
        public string TypeName {
            get => this.typeName;
            set => RaisePropertyChanged(ref this.typeName, value);
        }

        public AnnotationViewModel(AnnotationNode node) {
            this.node = node;
        }

        public void Load(AnnotationNode node) {
            this.TypeName = node.Type.ClassName.Name;
        }

        public void Save(AnnotationNode node) {

        }
    }
}
