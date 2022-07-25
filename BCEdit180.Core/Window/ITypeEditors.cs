using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCEdit180.Core.Editors;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180.Core.Dialogs {
    public interface ITypeEditors {
        Task<TypeDescriptor> EditTypeDescriptorDialog(TypeDescriptor defaultDescriptor = null);

        Task<MethodDescriptor> EditMethodDescriptorDialog(MethodDescriptor defaultDescriptor = null);

        Task<MethodEditorViewModel> EditMethodDialog(bool showMethodName = true, MethodEditorViewModel defaultEditor = null);

        Task<FieldEditorViewModel> EditFieldDialog(bool showFieldName = true, FieldEditorViewModel defaultEditor = null);

        Task<Opcode?> ChangeInstructionDialog(IEnumerable<Opcode> opcodes);
    }
}