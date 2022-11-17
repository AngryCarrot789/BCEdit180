using System;
using System.Collections.Generic;
using System.Windows;
using BCEdit180.Core;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Dialog;
using BCEdit180.Core.Dialog.Class;
using BCEdit180.Core.Dialog.Fields;
using BCEdit180.Core.Dialog.Methods;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Editors.Const;
using BCEdit180.Dialog.Class;
using BCEdit180.Dialog.Fields;
using BCEdit180.Dialog.Methods;
using BCEdit180.Windows;
using BCEdit180.Windows.Base;

namespace BCEdit180.Dialog {
    public class UIManager : IUIManager {
        private readonly Dictionary<Type, Type> modelToWindowMap;

        public UIManager() {
            this.modelToWindowMap = new Dictionary<Type, Type>();
            // class
            RegisterDialog<ClassAccessEditorViewModel, ClassAccessEditorDialog>();

            // method
            RegisterDialog<MethodAccessEditorViewModel, MethodAccessEditorDialog>();
            RegisterDialog<MethodDescEditorViewModel, MethodDescEditorDialog>();
            RegisterDialog<MethodEditorViewModel, MethodEditorDialog>();

            // field
            RegisterDialog<FieldAccessEditorViewModel, FieldAccessEditorDialog>();
            RegisterDialog<FieldEditorViewModel, FieldEditorWindow>();

            // general
            RegisterDialog<TypeEditorViewModel, TypeEditorDialog>();
            RegisterDialog<LabelSelectorViewModel, LabelSelectorWindow>();
            RegisterDialog<ConstValueEditorViewModel, ConstValueEditorWindow>();
            RegisterDialog<ChangeInstructionViewModel, ChangeInstructionWindow>();
        }

        private void RegisterDialog<TViewModel, TWindow>() where TViewModel : BaseDialogViewModel where TWindow : DialogBase {
            this.modelToWindowMap[typeof(TViewModel)] = typeof(TWindow);
        }

        public void ShowMessage(string title, string message) {
            MessageBox.Show(message, title);
        }

        public bool ShowDialog<T>(T input) where T : BaseDialogViewModel {
            if (this.modelToWindowMap.TryGetValue(typeof(T), out Type windowType)) {
                DialogBase dialog = (DialogBase) Activator.CreateInstance(windowType);
                dialog.DataContext = input;
                return dialog.ShowDialog(input);
            }
            else {
                throw new Exception("No UI window registered for dialog view model: " + typeof(T));
            }
        }
    }
}