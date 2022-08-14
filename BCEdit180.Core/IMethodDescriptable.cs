using System.Windows.Input;
using JavaAsm;

namespace BCEdit180.Core {
    public interface IMethodDescriptable {
        MethodDescriptor MethodDescriptor { get; set; }

        ICommand EditMethodDescriptorCommand { get; }
    }
}
