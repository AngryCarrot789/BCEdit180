using System.Threading.Tasks;
using JavaAsm;

namespace BCEdit180.Core.Dialogs {
    public interface IAccessEditor {
        /// <summary>
        /// Shows a dialog that allows a user to pick a class modifier
        /// </summary>
        Task<ClassAccessModifiers> EditClassAccess(ClassAccessModifiers defaultAccess = ClassAccessModifiers.Public | ClassAccessModifiers.Super);

        /// <summary>
        /// Shows a dialog that allows a user to pick a method modifier
        /// </summary>
        Task<MethodAccessModifiers> EditMethodAccess(MethodAccessModifiers defaultAccess = MethodAccessModifiers.Public);

        /// <summary>
        /// Shows a dialog that allows a user to pick a field modifier
        /// </summary>
        Task<FieldAccessModifiers> EditFieldAccess(FieldAccessModifiers defaultAccess = FieldAccessModifiers.Public);
    }
}