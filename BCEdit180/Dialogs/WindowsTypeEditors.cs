using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using BCEdit180.Windows;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180.Dialogs {
    public class WindowsTypeEditors : ITypeEditors {
        public Task<TypeDescriptor> EditTypeDescriptorDialog(TypeDescriptor defaultDescriptor = null) {
            TypeEditorWindow window = new TypeEditorWindow();
            TypeEditorViewModel editor = new TypeEditorViewModel();
            if (defaultDescriptor != null) {
                if (defaultDescriptor.PrimitiveType.HasValue) {
                    editor.IsPrimitive = true;
                    editor.SelectedPrimitive = defaultDescriptor.PrimitiveType.Value;
                }
                else {
                    editor.IsObject = true;
                    editor.ClassName = defaultDescriptor.ClassName?.Name;
                }

            }

            window.DataContext = editor;
            window.ShowDialog();
            if (editor.IsPrimitive) {
                return Task.FromResult(new TypeDescriptor(editor.SelectedPrimitive, editor.ArrayDepth));
            }
            else {
                return Task.FromResult(new TypeDescriptor(new ClassName(editor.RealClassName ?? ""), editor.ArrayDepth));
            }
        }

        public Task<MethodDescriptor> EditMethodDescriptorDialog(MethodDescriptor defaultDescriptor = null) {
            MethodDescEditorWindow window = new MethodDescEditorWindow();
            MethodEditorViewModel vm = new MethodEditorViewModel();
            if (defaultDescriptor != null) {
                vm.ReturnType = defaultDescriptor.ReturnType;
                vm.Parameters.Clear();
                if (defaultDescriptor.ArgumentTypes != null) {
                    vm.Parameters.AddAll(defaultDescriptor.ArgumentTypes.Select(x => new TypeDescriptorViewModel() {Descriptor = x}));
                }
            }

            window.DataContext = vm;
            window.ShowDialog();
            return Task.FromResult(new MethodDescriptor(vm.ReturnType, vm.Parameters.Select(x => x.Descriptor).ToList()));
        }

        public Task<MethodEditorViewModel> EditMethodDialog(bool showMethodName = true, MethodEditorViewModel defaultEditor = null) {
            Window window;
            if (showMethodName) {
                window = new MethodEditorWindow();
            }
            else {
                window = new MethodDescEditorWindow();
            }

            MethodEditorViewModel vm = new MethodEditorViewModel();
            if (defaultEditor != null) {
                vm.Access = defaultEditor.Access;
                vm.Parameters.AddAll(defaultEditor.Parameters);
                vm.MethodName = defaultEditor.MethodName;
                vm.ReturnType = defaultEditor.ReturnType;
            }

            window.DataContext = vm;
            window.ShowDialog();
            return Task.FromResult(vm);
        }

        public Task<FieldEditorViewModel> EditFieldDialog(bool showFieldName = true, FieldEditorViewModel defaultEditor = null) {
            FieldEditorWindow window = new FieldEditorWindow();
            FieldEditorViewModel vm = new FieldEditorViewModel();
            if (defaultEditor != null) {
                vm.Access = defaultEditor.Access;
                vm.FieldName = defaultEditor.FieldName;
                vm.Descriptor = defaultEditor.Descriptor;
            }

            window.DataContext = vm;
            window.ShowDialog();
            return Task.FromResult(vm);
        }

        public Task<Opcode?> ChangeInstructionDialog(IEnumerable<Opcode> opcodes) {
            ChangeInstructionWindow window = new ChangeInstructionWindow();
            ChangeInstructionViewModel vm = new ChangeInstructionViewModel();
            vm.SetAvailableInstructions(opcodes);
            window.DataContext = vm;
            window.Title = "Replace Opcode";
            window.ShowDialog();
            if (vm.IsValidSelection()) {
                return Task.FromResult<Opcode?>(vm.SelectedOpcode);
            }
            else {
                return Task.FromResult<Opcode?>(null);
            }
        }
    }
}