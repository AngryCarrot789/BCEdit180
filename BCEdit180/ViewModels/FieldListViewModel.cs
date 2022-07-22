using System.Collections.ObjectModel;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class FieldListViewModel : BaseViewModel {
        public ObservableCollection<FieldInfoViewModel> Fields { get; }

        public FieldListViewModel() {
            this.Fields = new ObservableCollection<FieldInfoViewModel>();
        }

        public void Load(ClassNode clazz) {
            this.Fields.Clear();
            foreach (FieldNode field in clazz.Fields) {
                this.Fields.Add(new FieldInfoViewModel(field));
            }
        }
    }
}
