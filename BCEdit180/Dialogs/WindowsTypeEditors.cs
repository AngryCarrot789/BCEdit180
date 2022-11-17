using System;
using System.Collections.Generic;
using System.Windows;
using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Window;
using BCEdit180.Windows;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180.Dialogs {
    public class WindowsTypeEditors : ITypeEditors {
        private static readonly Dictionary<Type, FlagEditorViewModel> FlagEditorForEnum = new Dictionary<Type, FlagEditorViewModel>();

        private static FlagEditorViewModel GetEditorForEnum<TEnum>() where TEnum : Enum {
            if (FlagEditorForEnum.TryGetValue(typeof(TEnum), out FlagEditorViewModel flagEditor)) {
                return flagEditor;
            }
            else {
                flagEditor = new FlagEditorViewModel(value => (TEnum) Enum.Parse(typeof(TEnum), value.ToString()));
                flagEditor.LoadFlags<TEnum>(m => Convert.ToInt64(m));
                return FlagEditorForEnum[typeof(TEnum)] = flagEditor;
            }
        }

        public bool EditFieldDialog(out FieldEditorViewModel editor, bool showFieldName = true) {
            return EditFieldDialog(null, out editor, showFieldName);
        }

        public bool EditFieldDialog(in FieldEditorViewModel template, out FieldEditorViewModel editor, bool showFieldName = true) {
            FieldEditorWindow window = new FieldEditorWindow();
            FieldEditorViewModel vm = template ?? new FieldEditorViewModel() {
                Access = FieldAccessModifiers.Public,
                Descriptor = new TypeDescriptor(PrimitiveType.Integer, 0),
                ConstantValue = 0,
                FieldName = "fieldName"
            };

            window.DataContext = vm;
            if (window.ShowDialog() != true || !(window.DataContext is FieldEditorViewModel)) {
                editor = null;
                return false;
            }

            editor = (FieldEditorViewModel) window.DataContext;
            return true;
        }

        public bool ChangeInstructionDialog(IEnumerable<Opcode> opcodes, out Opcode opcode) {
            return ChangeInstructionDialog(opcodes, null, out opcode);
        }

        public bool ChangeInstructionDialog(IEnumerable<Opcode> opcodes, in Opcode? defaultOpcode, out Opcode opcode) {
            ChangeInstructionWindow window = new ChangeInstructionWindow();
            window.Title = "Replace Opcode";

            using (ChangeInstructionViewModel vm = new ChangeInstructionViewModel()) {
                vm.SetAvailableInstructions(opcodes);
                window.DataContext = vm;
                if (defaultOpcode.HasValue) {
                    Opcode code = defaultOpcode.Value;
                    Application.Current.Dispatcher.Invoke(() => {
                        vm.SelectedOpcode = code;
                    }, System.Windows.Threading.DispatcherPriority.Loaded);
                }

                if (window.ShowDialog() != true || !vm.IsValidSelection()) {
                    opcode = Opcode.None;
                    return false;
                }

                opcode = vm.SelectedOpcode;
                return true;
            }
        }

        public bool EditEnumFlagDialog<TEnum>(in TEnum template, out TEnum access) where TEnum : Enum {
            FlagEditorWindow window = new FlagEditorWindow {Title = typeof(TEnum).Name + " Editor"};
            FlagEditorViewModel vm = GetEditorForEnum<TEnum>();
            vm.UpdateFlagItemsWithBitMask(Convert.ToInt64(template));
            window.DataContext = vm;
            if (window.ShowDialog() == true) {
                access = (TEnum) vm.GetEnumValue();
                return true;
            }
            else {
                access = template;
                return false;
            }
        }
    }
}