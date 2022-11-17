using System.Linq;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.CodeEditing.Descriptors;
using BCEdit180.Core.Dialog.Class;
using BCEdit180.Core.Dialog.Fields;
using BCEdit180.Core.Dialog.Methods;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Utils;
using JavaAsm;

namespace BCEdit180.Core.Dialog {
    public static class DialogUtils {
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

        public static bool EditType(out TypeDescriptor desc, bool allowPrimitve = true, bool allowClass = true) {
            return EditType(new TypeDescriptor(PrimitiveType.Integer, 0), out desc, allowPrimitve, allowClass);
        }

        public static bool EditType(in TypeDescriptor template, out TypeDescriptor desc, bool allowPrimitve = true, bool allowClass = true) {
            TypeEditorViewModel editor = new TypeEditorViewModel();
            editor.AllowPrimitive = allowPrimitve;
            editor.AllowClass = allowClass;
            if (template != null) {
                if (template.PrimitiveType.HasValue && allowPrimitve) {
                    editor.IsPrimitive = true;
                    editor.SelectedPrimitive = template.PrimitiveType.Value;
                }
                else {
                    editor.IsObject = true;
                    editor.InputClassName = template.ClassName?.Name;
                }

                editor.ArrayDepth = (ushort) template.ArrayDepth;
            }

            if (IoC.UI.ShowDialog(editor)) {
                if (editor.IsPrimitive) {
                    desc = new TypeDescriptor(editor.SelectedPrimitive, editor.ArrayDepth);
                }
                else if (string.IsNullOrEmpty(editor.InputClassName)) {
                    desc = null;
                    return false;
                }
                else {
                    desc = new TypeDescriptor(new ClassName(editor.GetInternalName() ?? ""), editor.ArrayDepth);
                }

                return true;
            }
            else {
                desc = null;
                return false;
            }
        }

        public static bool EditMethodDesc(in MethodDescriptor template, out MethodDescriptor desc) {
            MethodDescEditorViewModel editor = new MethodEditorViewModel();
            if (template != null) {
                editor.ReturnType = template.ReturnType;
                if (template.ArgumentTypes != null) {
                    editor.Parameters.AddAll(template.ArgumentTypes.Select(x => new TypeDescViewModel(x)));
                }
            }

            if (IoC.UI.ShowDialog(editor)) {
                desc = editor.Descriptor;
                return true;
            }
            else {
                desc = null;
                return false;
            }
        }

        public static bool EditMethod(out MethodEditorViewModel methodEditorViewModel, bool showMethodName = true) {
            MethodEditorViewModel vm = new MethodEditorViewModel() {
                ReturnType = new TypeDescriptor(PrimitiveType.Void, 0),
                Access = MethodAccessModifiers.Public,
                MethodName = "methodName"
            };

            if (IoC.UI.ShowDialog(vm)) {
                methodEditorViewModel = vm;
                return true;
            }
            else {
                methodEditorViewModel = null;
                return false;
            }
        }

        public static bool SelectLabelDialog(BytecodeEditorViewModel editor, out LabelViewModel label) {
            LabelSelectorViewModel vm = new LabelSelectorViewModel {BytecodeEditor = editor};
            if (IoC.UI.ShowDialog(vm) && vm.SelectedInstruction is LabelViewModel selectedLabel) {
                label = selectedLabel;
                return true;
            }
            else {
                label = null;
                return false;
            }
        }

        public static bool EditConstantDialog(in ConstValueEditorViewModel template, out ConstValueEditorViewModel model) {
            ConstValueEditorViewModel vm = template ?? new ConstValueEditorViewModel() {
                Type = ConstType.String,
                ValueString = "Constant String Value Here"
            };

            if (IoC.UI.ShowDialog(vm)) {
                model = vm;
                return true;
            }
            else {
                model = null;
                return false;
            }
        }
    }
}