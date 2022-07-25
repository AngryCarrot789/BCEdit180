using System.Collections.Generic;
using System.Threading.Tasks;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Editors.Const;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180.Core.Window {
    public interface ITypeEditors {
        Task<TypeDescriptor> EditTypeDescriptorDialog(TypeDescriptor defaultDescriptor = null, bool allowClass = true, bool allowPrimitve = true);

        Task<MethodDescriptor> EditMethodDescriptorDialog(MethodDescriptor defaultDescriptor = null);

        Task<MethodEditorViewModel> EditMethodDialog(bool showMethodName = true, MethodEditorViewModel defaultEditor = null);

        Task<FieldEditorViewModel> EditFieldDialog(bool showFieldName = true, FieldEditorViewModel defaultEditor = null);

        Task<ConstValueEditorViewModel> EditConstantDialog(ConstValueEditorViewModel defaultEditor = null);

        Task<Opcode?> ChangeInstructionDialog(IEnumerable<Opcode> opcodes);
    }
}