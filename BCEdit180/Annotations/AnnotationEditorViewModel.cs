using System.Collections.ObjectModel;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Annotations {
    public class AnnotationEditorViewModel : BaseViewModel {
        public ObservableCollection<AnnotationViewModel> Annotations { get; }

        public AnnotationEditorViewModel() {
            this.Annotations = new ObservableCollection<AnnotationViewModel>();
        }
    }
}
