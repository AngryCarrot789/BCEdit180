using System.Collections.ObjectModel;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class MethodListViewModel : BaseViewModel {
        public ObservableCollection<MethodInfoViewModel> Methods { get; }

        public MethodListViewModel() {
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
