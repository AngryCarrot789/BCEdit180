using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Core.Utils;
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
                return FlagEditorForEnum[typeof(TEnum)] = new FlagEditorViewModel(value => (TEnum) Enum.Parse(typeof(TEnum), value.ToString()));
            }
        }

        public Task<bool> EditTypeDescriptorDialog(out TypeDescriptor descriptor, bool allowClass = true, bool allowPrimitve = true) {
            return EditTypeDescriptorDialog(new TypeDescriptor(PrimitiveType.Integer, 0), out descriptor, allowClass, allowPrimitve);
        }

        public Task<bool> EditTypeDescriptorDialog(in TypeDescriptor template, out TypeDescriptor descriptor, bool allowClass = true, bool allowPrimitve = true) {
            TypeEditorWindow window = new TypeEditorWindow();
            TypeEditorViewModel editor = new TypeEditorViewModel();
            if (template != null) {
                if (template.PrimitiveType.HasValue) {
                    editor.IsPrimitive = true;
                    editor.SelectedPrimitive = template.PrimitiveType.Value;
                }
                else {
                    editor.IsObject = true;
                    editor.ClassName = template.ClassName?.Name;
                }
            }

            editor.AllowPrimitive = allowPrimitve;
            editor.AllowClass = allowClass;
            window.DataContext = editor;
            if (window.ShowDialog() != true) {
                descriptor = null;
                return Task.FromResult(false);
            }

            if (editor.IsPrimitive) {
                descriptor = new TypeDescriptor(editor.SelectedPrimitive, editor.ArrayDepth);
            }
            else {
                descriptor = new TypeDescriptor(new ClassName(editor.RealClassName ?? ""), editor.ArrayDepth);
            }

            return Task.FromResult(true);
        }

        public Task<bool> EditMethodDescriptorDialog(out MethodDescriptor descriptor) {
            return EditMethodDescriptorDialog(new MethodDescriptor(new TypeDescriptor(PrimitiveType.Void, 0), new List<TypeDescriptor>()), out descriptor);
        }

        public Task<bool> EditMethodDescriptorDialog(in MethodDescriptor template, out MethodDescriptor descriptor) {
            MethodDescEditorWindow window = new MethodDescEditorWindow();
            MethodEditorViewModel vm = new MethodEditorViewModel();
            if (template != null) {
                vm.ReturnType = template.ReturnType;
                vm.Parameters.Clear();
                if (template.ArgumentTypes != null) {
                    vm.Parameters.AddAll(template.ArgumentTypes.Select(x => new TypeDescriptorViewModel() { Descriptor = x }));
                }
            }

            window.DataContext = vm;
            if (window.ShowDialog() != true) {
                descriptor = null;
                return Task.FromResult(false);
            }

            descriptor = new MethodDescriptor(vm.ReturnType ?? new TypeDescriptor(PrimitiveType.Void, 0), vm.Parameters.Select(x => x.Descriptor).ToList());
            return Task.FromResult(true);
        }

        public Task<bool> EditMethodDialog(out MethodEditorViewModel editor, bool showMethodName = true) {
            return EditMethodDialog(null, out editor, showMethodName);
        }

        public Task<bool> EditMethodDialog(in MethodEditorViewModel template, out MethodEditorViewModel editor, bool showMethodName = true) {
            Window window;
            if (showMethodName) {
                window = new MethodEditorWindow();
            }
            else {
                window = new MethodDescEditorWindow();
            }

            MethodEditorViewModel vm = template ?? new MethodEditorViewModel() {
                ReturnType = new TypeDescriptor(PrimitiveType.Void, 0),
                Access = MethodAccessModifiers.Public,
                MethodName = "methodName"
            };

            window.DataContext = vm;
            if (window.ShowDialog() != true || !(window.DataContext is MethodEditorViewModel)) {
                editor = null;
                return Task.FromResult(false);
            }

            editor = (MethodEditorViewModel) window.DataContext;
            return Task.FromResult(true);
        }

        public Task<bool> EditFieldDialog(out FieldEditorViewModel editor, bool showFieldName = true) {
            return EditFieldDialog(null, out editor, showFieldName);
        }

        public Task<bool> EditFieldDialog(in FieldEditorViewModel template, out FieldEditorViewModel editor, bool showFieldName = true) {
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
                return Task.FromResult(false);
            }

            editor = (FieldEditorViewModel) window.DataContext;
            return Task.FromResult(true);
        }

        public Task<bool> EditConstantDialog(out ConstValueEditorViewModel editor) {
            return EditConstantDialog(null, out editor);
        }

        public Task<bool> EditConstantDialog(in ConstValueEditorViewModel template, out ConstValueEditorViewModel editor) {
            ConstValueEditorWindow window = new ConstValueEditorWindow();
            ConstValueEditorViewModel vm = template ?? new ConstValueEditorViewModel() {
                Type = ConstType.String,
                ValueString = "Constant String Value Here"
            };

            window.DataContext = vm;
            if (window.ShowDialog() != true || !(window.DataContext is ConstValueEditorViewModel)) {
                editor = null;
                return Task.FromResult(false);
            }

            editor = (ConstValueEditorViewModel) window.DataContext;
            return Task.FromResult(true);
        }

        public Task<bool> ChangeInstructionDialog(IEnumerable<Opcode> opcodes, out Opcode opcode) {
            return ChangeInstructionDialog(opcodes, null, out opcode);
        }

        public Task<bool> ChangeInstructionDialog(IEnumerable<Opcode> opcodes, in Opcode? defaultOpcode, out Opcode opcode) {
            ChangeInstructionWindow window = new ChangeInstructionWindow();
            window.Title = "Replace Opcode";

            ChangeInstructionViewModel vm = new ChangeInstructionViewModel();
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
                return Task.FromResult(false);
            }

            opcode = vm.SelectedOpcode;
            return Task.FromResult(true);
        }

        public Task<bool> EditEnumDialog<TEnum>(out TEnum access) where TEnum : Enum {
            return EditEnumDialog(default, out access);
        }

        public Task<bool> EditEnumDialog<TEnum>(in TEnum template, out TEnum access) where TEnum : Enum {
            FlagEditorWindow window = new FlagEditorWindow {Title = typeof(TEnum).Name + " Editor"};
            FlagEditorViewModel vm = GetEditorForEnum<TEnum>();
            vm.UpdateFlagItemsWithBitMask(Convert.ToInt64(template));
            window.DataContext = vm;
            if (window.ShowDialog() == true) {
                access = (TEnum) vm.GetEnumValue();
                return Task.FromResult(true);
            }
            else {
                access = template;
                return Task.FromResult(false);
            }
        }
    }
}