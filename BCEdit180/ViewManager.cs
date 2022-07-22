using System;
using System.Collections.Generic;
using System.Reflection;
using BCEdit180.CodeEditing.InstructionEdit;
using BCEdit180.FlagEditor;
using BCEdit180.ViewModels;
using JavaAsm;
using JavaAsm.Instructions;

namespace BCEdit180 {
    public class ViewManager {
        private static readonly FlagEditorViewModel ClassInfoFlagEditorVM;
        private static readonly FlagEditorViewModel MethodInfoFlagEditorVM;
        private static readonly FlagEditorViewModel FieldInfoFlagEditorVM;

        static ViewManager() {
            ClassInfoFlagEditorVM = new FlagEditorViewModel(value => (ClassAccessModifiers) value);
            ClassInfoFlagEditorVM.LoadFlags<ClassAccessModifiers>(m => (long) m);

            MethodInfoFlagEditorVM = new FlagEditorViewModel(value => (MethodAccessModifiers) value);
            MethodInfoFlagEditorVM.LoadFlags<MethodAccessModifiers>(m => (long) m);

            FieldInfoFlagEditorVM = new FlagEditorViewModel(value => (FieldAccessModifiers) value);
            FieldInfoFlagEditorVM.LoadFlags<FieldAccessModifiers>(m => (long) m);
        }

        public static void ShowAccessEditor(MethodInfoViewModel model) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Method Access Editor";
            MethodInfoFlagEditorVM.UpdateFlagItemsWithBitMask<MethodAccessModifiers>((long) model.Access);
            MethodInfoFlagEditorVM.UpdateEnumCallback = (e) => model.Access = (MethodAccessModifiers) e;
            window.DataContext = MethodInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(FieldInfoViewModel model) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Field Access Editor";
            FieldInfoFlagEditorVM.UpdateFlagItemsWithBitMask<FieldAccessModifiers>((long) model.Access);
            FieldInfoFlagEditorVM.UpdateEnumCallback = (e) => model.Access = (FieldAccessModifiers) e;
            window.DataContext = FieldInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(ClassInfoViewModel model) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Method Access Editor";
            // ClassInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) model.Access);
            // ClassInfoFlagEditorVM.UpdateEnumCallback = (e) => model.Access = (MethodAccessModifiers) e;
            window.DataContext = ClassInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowDescriptorEditor(TypeDescriptor descriptor) {

        }

        public static void ShowEditInstructionView(IEnumerable<Opcode> opcodes, Action<Opcode> callback) {
            ChangeInstructionWindow window = new ChangeInstructionWindow();
            ChangeInstructionViewModel vm = new ChangeInstructionViewModel();
            vm.SetOpcodeCallback = callback;
            vm.SetAvailableInstructions(opcodes);
            window.DataContext = vm;
            window.Title = "Replace Opcode";
            window.ShowDialog();
        }
    }
}