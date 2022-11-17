using System;
using System.Collections.Generic;
using BCEdit180.Core.Editors;
using JavaAsm.Instructions;

namespace BCEdit180.Core.Window {
    public interface ITypeEditors {
        bool EditFieldDialog(out FieldEditorViewModel editor, bool showFieldName = true);

        bool ChangeInstructionDialog(IEnumerable<Opcode> opcodes, out Opcode opcode);

        bool ChangeInstructionDialog(IEnumerable<Opcode> opcodes, in Opcode? defaultOpcode, out Opcode opcode);

        bool EditEnumFlagDialog<TEnum>(in TEnum template, out TEnum access) where TEnum : Enum ;
    }
}