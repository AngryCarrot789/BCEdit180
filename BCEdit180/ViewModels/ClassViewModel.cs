using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ClassViewModel : BaseViewModel {
        public ClassInfoViewModel ClassInfo { get; private set; }
        public MethodListViewModel MethodList { get; private set; }
        public FieldListViewModel FieldList { get; private set; }

        public ClassViewModel(ClassNode node) {
            this.ClassInfo = new ClassInfoViewModel();
            this.ClassInfo.Update(node);

            this.MethodList = new MethodListViewModel();
            this.MethodList.Update(node);

            this.FieldList = new FieldListViewModel();
            this.FieldList.Update(node);
        }
    }
}