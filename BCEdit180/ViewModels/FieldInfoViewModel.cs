using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class FieldInfoViewModel : BaseViewModel {
        private readonly FieldNode field;

        private string fieldName;
        public string FieldName {
            get => this.fieldName;
            set => RaisePropertyChanged(ref this.fieldName, value);
        }

        public FieldInfoViewModel(FieldNode field) {
            this.field = field;
        }

        public void Load(MethodNode node) {
            this.FieldName = node.Name;
        }

        public void Save(FieldNode node) {
            node.Name = this.FieldName;
        }
    }
}
