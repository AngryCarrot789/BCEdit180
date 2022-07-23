using System.ComponentModel;
using REghZy.MVVM.ViewModels;
using REghZy.MVVM.Views;
using TypeDescriptor = JavaAsm.TypeDescriptor;

namespace BCEdit180.MethodCreator {
    public class TypeDescriptorViewModel : BaseViewModel {
        private TypeDescriptor descriptor;

        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }
    }
}