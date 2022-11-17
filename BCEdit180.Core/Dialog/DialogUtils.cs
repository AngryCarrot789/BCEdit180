using BCEdit180.Core.Dialog.Class;
using BCEdit180.Core.Dialog.Fields;
using BCEdit180.Core.Dialog.Methods;
using JavaAsm;

namespace BCEdit180.Core.Dialog {
    public static class AccessDialogs {
        public static bool ShowClassAcccessDialog(in ClassAccessModifiers template, out ClassAccessModifiers modifiers) {
            ClassAccessEditorViewModel editor = new ClassAccessEditorViewModel() {
                Modifiers = template
            };

            if (IoC.UI.ShowDialog(editor)) {
                modifiers = editor.Modifiers;
                return true;
            }
            else {
                modifiers = default;
                return false;
            }
        }

        public static bool ShowMethodAcccessDialog(in MethodAccessModifiers template, out MethodAccessModifiers modifiers) {
            MethodAccessEditorViewModel editor = new MethodAccessEditorViewModel() {
                Modifiers = template
            };

            if (IoC.UI.ShowDialog(editor)) {
                modifiers = editor.Modifiers;
                return true;
            }
            else {
                modifiers = default;
                return false;
            }
        }

        public static bool ShowFieldAcccessDialog(in FieldAccessModifiers template, out FieldAccessModifiers modifiers) {
            FieldAccessEditorViewModel editor = new FieldAccessEditorViewModel() {
                Modifiers = template
            };

            if (IoC.UI.ShowDialog(editor)) {
                modifiers = editor.Modifiers;
                return true;
            }
            else {
                modifiers = default;
                return false;
            }
        }
    }
}