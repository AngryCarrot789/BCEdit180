using System.Windows.Input;
using JavaAsm;

namespace BCEdit180.Core {
    public interface IFieldDescriptable {
        TypeDescriptor FieldDescriptor { get; set; }

        ICommand EditFieldDescriptorCommand { get; }
    }
}
