using System.Collections.ObjectModel;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodListViewModel : BaseViewModel {
        public ClassViewModel Class { get; }

        public ObservableCollection<MethodInfoViewModel> Methods { get; }

        private MethodInfoViewModel selectedMethod;
        public MethodInfoViewModel SelectedMethod {
            get => this.selectedMethod;
            set => RaisePropertyChanged(ref this.selectedMethod, value);
        }

        public MethodListViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            this.Methods = new ObservableCollection<MethodInfoViewModel>();
        }

        public void Load(ClassNode clazz) {
            this.Methods.Clear();
            foreach (MethodNode method in clazz.Methods) {
                this.Methods.Add(new MethodInfoViewModel(method));
            }
        }

        public void Save(ClassNode node) {
            foreach (MethodInfoViewModel method in this.Methods) {
                method.Save(method.Node);
            }
        }
    }
}
