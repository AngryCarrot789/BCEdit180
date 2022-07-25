using System.Threading.Tasks;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Window;
using BCEdit180.Windows;
using JavaAsm;

namespace BCEdit180.Dialogs {
    public class WindowsAccessEditor : IAccessEditor {
        private static readonly FlagEditorViewModel ClassInfoFlagEditorVM;
        private static readonly FlagEditorViewModel MethodInfoFlagEditorVM;
        private static readonly FlagEditorViewModel FieldInfoFlagEditorVM;

        static WindowsAccessEditor() {
            ClassInfoFlagEditorVM = new FlagEditorViewModel(value => (ClassAccessModifiers) value);
            ClassInfoFlagEditorVM.LoadFlags<ClassAccessModifiers>(m => (long) m);

            MethodInfoFlagEditorVM = new FlagEditorViewModel(value => (MethodAccessModifiers) value);
            MethodInfoFlagEditorVM.LoadFlags<MethodAccessModifiers>(m => (long) m);

            FieldInfoFlagEditorVM = new FlagEditorViewModel(value => (FieldAccessModifiers) value);
            FieldInfoFlagEditorVM.LoadFlags<FieldAccessModifiers>(m => (long) m);
        }

        public Task<ClassAccessModifiers?> EditClassAccess(ClassAccessModifiers defaultAccess = ClassAccessModifiers.Public | ClassAccessModifiers.Super) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Class Access"};
            ClassInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) defaultAccess);
            window.DataContext = ClassInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                return Task.FromResult((ClassAccessModifiers?) ClassInfoFlagEditorVM.GetEnumValue());
            }
            else {
                return Task.FromResult<ClassAccessModifiers?>(null);
            }
        }

        public Task<MethodAccessModifiers?> EditMethodAccess(MethodAccessModifiers defaultAccess = MethodAccessModifiers.Public) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Method Access"};
            MethodInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) defaultAccess);
            window.DataContext = MethodInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                return Task.FromResult((MethodAccessModifiers?) MethodInfoFlagEditorVM.GetEnumValue());
            }
            else {
                return Task.FromResult<MethodAccessModifiers?>(null);
            }
        }

        public Task<FieldAccessModifiers?> EditFieldAccess(FieldAccessModifiers defaultAccess = FieldAccessModifiers.Public) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Field Access"};
            FieldInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) defaultAccess);
            window.DataContext = FieldInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                return Task.FromResult((FieldAccessModifiers?) FieldInfoFlagEditorVM.GetEnumValue());
            }
            else {
                return Task.FromResult<FieldAccessModifiers?>(null);
            }
        }
    }
}