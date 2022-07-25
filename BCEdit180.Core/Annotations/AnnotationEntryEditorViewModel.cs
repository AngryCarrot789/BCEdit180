using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Annotations {
    public class AnnotationEntryEditorViewModel : BaseViewModel {
        private ElementValueTagXAML selectedPrimitive;
        public ElementValueTagXAML SelectedPrimitive {
            get => this.selectedPrimitive;
            set => RaisePropertyChanged(ref this.selectedPrimitive, value);
        }
    }
}