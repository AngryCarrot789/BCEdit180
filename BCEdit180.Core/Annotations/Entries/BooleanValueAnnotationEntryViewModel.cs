using JavaAsm.CustomAttributes.Annotation;

namespace BCEdit180.Annotations.Entries {
    public class BooleanValueAnnotationEntryViewModel : BaseAnnotationEntryViewModel {
        private bool state;

        public bool State {
            get => this.state;
            set {
                RaisePropertyChanged(ref this.state, value);
                this.value.ConstValue = this.value;
            }
        }

        public BooleanValueAnnotationEntryViewModel(AnnotationViewModel annotation, AnnotationNode.ElementValuePair entry) : base(annotation, entry) {

        }

        public override void Load(AnnotationNode.ElementValuePair entry) {
            base.Load(entry);
            this.State = bool.TryParse(entry.Value.ConstValue?.ToString() ?? "False", out bool bVal) && bVal;
        }

        public override void Save(AnnotationNode.ElementValuePair entry) {
            base.Save(entry);
            entry.Value.ConstValue = this.State;
        }
    }
}