using System.Threading.Tasks;
using JavaAsm;

namespace BCEdit180.Core.Window {
    public interface IAccessEditor {
        Task<bool> EditClassAccess(out ClassAccessModifiers access);
        Task<bool> EditClassAccess(in ClassAccessModifiers template, out ClassAccessModifiers access);

        Task<bool> EditMethodAccess(out MethodAccessModifiers template);
        Task<bool> EditMethodAccess(in MethodAccessModifiers template, out MethodAccessModifiers access);

        Task<bool> EditFieldAccess(out FieldAccessModifiers access);
        Task<bool> EditFieldAccess(in FieldAccessModifiers template, out FieldAccessModifiers access);
    }
}