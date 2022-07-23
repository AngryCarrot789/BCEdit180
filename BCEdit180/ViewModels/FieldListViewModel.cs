using System.Collections.ObjectModel;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class FieldListViewModel : BaseViewModel {
        public ClassViewModel Class { get; }

        public ObservableCollection<FieldInfoViewModel> Fields { get; }

        private FieldInfoViewModel selectedField;
        public FieldInfoViewModel SelectedField {
            get => this.selectedField;
            set => RaisePropertyChanged(ref this.selectedField, value);
        }

        public FieldListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Fields = new ObservableCollection<FieldInfoViewModel>();
        }

        public void Load(ClassNode clazz) {
            this.Fields.Clear();
            foreach (FieldNode field in clazz.Fields) {
                this.Fields.Add(new FieldInfoViewModel(field));
            }
        }

        public void Save(ClassNode node) {
            foreach (FieldInfoViewModel field in this.Fields) {
                field.Save(field.Node);
            }
        }
    }
}
