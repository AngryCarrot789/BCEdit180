using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Editors.Const;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180.Core.Window {
    public interface ITypeEditors {
        Task<bool> EditTypeDescriptorDialog(out TypeDescriptor descriptor, bool allowClass = true, bool allowPrimitve = true);

        Task<bool> EditTypeDescriptorDialog(in TypeDescriptor template, out TypeDescriptor descriptor, bool allowClass = true, bool allowPrimitve = true);

        Task<bool> EditMethodDescriptorDialog(out MethodDescriptor descriptor);

        Task<bool> EditMethodDescriptorDialog(in MethodDescriptor template, out MethodDescriptor descriptor);

        Task<bool> EditMethodDialog(out MethodEditorViewModel editor, bool showMethodName = true);

        Task<bool> EditMethodDialog(in MethodEditorViewModel template, out MethodEditorViewModel editor, bool showMethodName = true);

        Task<bool> EditFieldDialog(out FieldEditorViewModel editor, bool showFieldName = true);

        Task<bool> EditFieldDialog(in FieldEditorViewModel template, out FieldEditorViewModel editor, bool showFieldName = true);

        Task<bool> EditConstantDialog(out ConstValueEditorViewModel editor);

        Task<bool> EditConstantDialog(in ConstValueEditorViewModel template, out ConstValueEditorViewModel editor);

        Task<bool> ChangeInstructionDialog(IEnumerable<Opcode> opcodes, out Opcode opcode);

        Task<bool> ChangeInstructionDialog(IEnumerable<Opcode> opcodes, in Opcode? defaultOpcode, out Opcode opcode);

        Task<bool> EditEnumDialog<TEnum>(out TEnum access) where TEnum : Enum ;

        Task<bool> EditEnumDialog<TEnum>(in TEnum template, out TEnum access) where TEnum : Enum ;
    }
}