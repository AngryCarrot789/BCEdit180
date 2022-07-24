using System;
using System.Collections.Generic;
using System.Linq;
using BCEdit180.CodeEditing.InstructionEdit;
using BCEdit180.FlagEditor;
using BCEdit180.MethodCreator;
using BCEdit180.TypeEditor;
using BCEdit180.Utils;
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
            window.Title = "Method Access";
            MethodInfoFlagEditorVM.UpdateFlagItemsWithBitMask<MethodAccessModifiers>((long) model.Access);
            MethodInfoFlagEditorVM.ApplyChangeCallback = (e) => model.Access = (MethodAccessModifiers) e;
            window.DataContext = MethodInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(Action<MethodAccessModifiers> accessCallback, MethodAccessModifiers defaultAccess = MethodAccessModifiers.Public) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Method Access";
            MethodInfoFlagEditorVM.UpdateFlagItemsWithBitMask<MethodAccessModifiers>((long) defaultAccess);
            MethodInfoFlagEditorVM.ApplyChangeCallback = (e) => accessCallback((MethodAccessModifiers) e);
            window.DataContext = MethodInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(FieldInfoViewModel model) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Field Access";
            FieldInfoFlagEditorVM.UpdateFlagItemsWithBitMask<FieldAccessModifiers>((long) model.Access);
            FieldInfoFlagEditorVM.ApplyChangeCallback = (e) => model.Access = (FieldAccessModifiers) e;
            window.DataContext = FieldInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(Action<FieldAccessModifiers> accessCallback, FieldAccessModifiers defaultAccess = FieldAccessModifiers.Public) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Field Access";
            FieldInfoFlagEditorVM.UpdateFlagItemsWithBitMask<FieldAccessModifiers>((long) defaultAccess);
            FieldInfoFlagEditorVM.ApplyChangeCallback = (e) => accessCallback((FieldAccessModifiers) e);
            window.DataContext = FieldInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(ClassInfoViewModel model) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Class Access";
            ClassInfoFlagEditorVM.UpdateFlagItemsWithBitMask<ClassAccessModifiers>((long) model.AccessFlags);
            ClassInfoFlagEditorVM.ApplyChangeCallback = (e) => model.AccessFlags = (ClassAccessModifiers) e;
            window.DataContext = ClassInfoFlagEditorVM;
            window.ShowDialog();
        }

        public static void ShowAccessEditor(Action<ClassAccessModifiers> accessCallback, ClassAccessModifiers defaultAccess = ClassAccessModifiers.Public) {
            FlagEditorWindow window = new FlagEditorWindow();
            window.Title = "Class Access";
            ClassInfoFlagEditorVM.UpdateFlagItemsWithBitMask<ClassAccessModifiers>((long) defaultAccess);
            ClassInfoFlagEditorVM.ApplyChangeCallback = (e) => accessCallback((ClassAccessModifiers) e);
            window.DataContext = ClassInfoFlagEditorVM;
            window.ShowDialog();
        }

        internal static void ShowMethodDescriptorEditorRT(MethodInfoViewModel method) {
            ShowDescriptorEditor(method.Descriptor.ReturnType, (t, c, a) => {
                method.Descriptor = t.HasValue ? new MethodDescriptor(new TypeDescriptor(t.Value, a), method.Descriptor.ArgumentTypes) : new MethodDescriptor(new TypeDescriptor(new ClassName(c), a), method.Descriptor.ArgumentTypes);
            });
        }

        internal static void ShowDescriptorEditor(FieldInfoViewModel field) {
            ShowDescriptorEditor(field.Descriptor, (t, c, a) => {
                field.Descriptor = t.HasValue ? new TypeDescriptor(t.Value, a) : new TypeDescriptor(new ClassName(c), a);
            });
        }

        public static void ShowDescriptorEditor(TypeDescriptor descriptor, Action<PrimitiveType?, string, int> callback) {
            ShowEditType(callback, descriptor?.PrimitiveType, descriptor?.ClassName?.Name);
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

        public static void ShowCreateMethod(Action<MethodEditorViewModel> callback) {
            MethodEditorWindow window = new MethodEditorWindow();
            MethodEditorViewModel vm = new MethodEditorViewModel();
            // i love lambdas
            vm.Callback = () => { callback(vm); };
            window.DataContext = vm;
            window.ShowDialog();
        }

        public static void ShowEditMethodDesc(Action<MethodDescriptor> callback, TypeDescriptor retType = null, List<TypeDescriptor> param = null) {
            MethodEditorWindow window = new MethodEditorWindow();
            MethodEditorViewModel vm = new MethodEditorViewModel();
            vm.Callback = () => { 
                callback(new MethodDescriptor(vm.ReturnType, vm.Parameters.Select(x => x.Descriptor).ToList())); 
            };
            vm.ReturnType = retType;
            vm.Parameters.Clear();
            if (param != null) {
                vm.Parameters.AddAll(param.Select(x => new TypeDescriptorViewModel() { Descriptor = x }));
            }

            window.DataContext = vm;
            window.ShowDialog();
        }

        public static void ShowEditType(Action<PrimitiveType?, string, int> callback, PrimitiveType? defaultPrimitive = null, string defaultClass = null) {
            TypeEditorWindow window = new TypeEditorWindow();
            TypeEditorViewModel editor = new TypeEditorViewModel();
            if (defaultPrimitive.HasValue) {
                editor.IsPrimitive = true;
                editor.SelectedPrimitive = defaultPrimitive.Value;
            }
            else {
                editor.IsObject = true;
                if (defaultClass != null) {
                    editor.ClassName = defaultClass;
                }
            }

            editor.Callback = callback;
            window.DataContext = editor;
            window.ShowDialog();
        }
    }
}