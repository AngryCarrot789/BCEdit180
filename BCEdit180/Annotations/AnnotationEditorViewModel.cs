using System.Collections.ObjectModel;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Annotations {
    public class AnnotationEditorViewModel : BaseViewModel {
        private ObservableCollection<AnnotationViewModel> annotations;
        public ObservableCollection<AnnotationViewModel> Annotations {
            get => this.annotations;
            set => RaisePropertyChanged(ref this.annotations, value);
        }

        public AnnotationEditorViewModel() {
            this.Annotations = new ObservableCollection<AnnotationViewModel>();
        }

        public AnnotationEditorViewModel(ObservableCollection<AnnotationViewModel> annotations) {
            this.Annotations = annotations;
        }
    }
}
