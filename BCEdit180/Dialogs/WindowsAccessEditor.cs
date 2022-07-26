using System.Security.Policy;
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

        public Task<bool> EditClassAccess(out ClassAccessModifiers access) {
            return EditClassAccess(ClassAccessModifiers.Public | ClassAccessModifiers.Super, out access);
        }

        public Task<bool> EditClassAccess(in ClassAccessModifiers template, out ClassAccessModifiers access) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Class Access"};
            ClassInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) template);
            window.DataContext = ClassInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                access = (ClassAccessModifiers) ClassInfoFlagEditorVM.GetEnumValue();
                return Task.FromResult(true);
            }
            else {
                access = template;
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditMethodAccess(out MethodAccessModifiers access) {
            return EditMethodAccess(MethodAccessModifiers.Public, out access);
        }

        public Task<bool> EditMethodAccess(in MethodAccessModifiers template, out MethodAccessModifiers access) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Method Access"};
            MethodInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) template);
            window.DataContext = MethodInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                access = (MethodAccessModifiers) MethodInfoFlagEditorVM.GetEnumValue();
                return Task.FromResult(true);
            }
            else {
                access = template;
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditFieldAccess(out FieldAccessModifiers access) {
            return EditFieldAccess(FieldAccessModifiers.Public, out access);
        }

        public Task<bool> EditFieldAccess(in FieldAccessModifiers template, out FieldAccessModifiers access) {
            FlagEditorWindow window = new FlagEditorWindow {Title = "Field Access"};
            FieldInfoFlagEditorVM.UpdateFlagItemsWithBitMask((long) template);
            window.DataContext = FieldInfoFlagEditorVM;
            if (window.ShowDialog() == true) {
                access = (FieldAccessModifiers) FieldInfoFlagEditorVM.GetEnumValue();
                return Task.FromResult(true);
            }
            else {
                access = template;
                return Task.FromResult(false);
            }
        }
    }
}