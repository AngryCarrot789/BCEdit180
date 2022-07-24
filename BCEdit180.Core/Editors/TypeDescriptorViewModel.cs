using REghZy.MVVM.ViewModels;
using TypeDescriptor = JavaAsm.TypeDescriptor;

namespace BCEdit180.Core.Editors {
    public class TypeDescriptorViewModel : BaseViewModel {
        private TypeDescriptor descriptor;

        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }
    }
}