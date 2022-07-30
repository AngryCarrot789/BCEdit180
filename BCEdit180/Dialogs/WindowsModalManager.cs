using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCEdit180.Core.ClasspathEditor;
using BCEdit180.Core.CodeEditing.InstructionEdit;
using BCEdit180.Core.Editors;
using BCEdit180.Core.Modals;
using BCEdit180.Windows;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Dialogs {
    public class WindowsModalManager : IModalManager {
        private static readonly Dictionary<Type, Type> ViewModelToWindow = new Dictionary<Type, Type>();
        
        static WindowsModalManager() {
            RegisterType<ChangeInstructionViewModel, ChangeInstructionWindow>();
            RegisterType<FieldEditorViewModel,       FieldEditorWindow>();
            RegisterType<MethodDescEditorViewModel,  MethodDescEditorWindow>();
            RegisterType<MethodEditorViewModel,      MethodEditorWindow>();
            RegisterType<TypeEditorViewModel,        TypeEditorWindow>();
            RegisterType<ClassPathListViewModel,     ClassPathListWindow>();
        }

        private static void RegisterType<TViewModel, TWindow>() where TViewModel : BaseViewModel, new() where TWindow : WindowModal {
            ViewModelToWindow[typeof(TViewModel)] = typeof(TWindow);
        }

        public Task<bool> ShowDialog<T>(out T result) where T : BaseViewModel, new() {
            if (ViewModelToWindow.TryGetValue(typeof(T), out Type windowType)) {
                WindowModal window = (WindowModal) Activator.CreateInstance(windowType);
                T vm = new T();
                window.DataContext = vm;
                if (window.ShowDialog() == true && window.DataContext is T t) {
                    result = t;
                    return Task.FromResult(true);
                }
                else {
                    result = null;
                    return Task.FromResult(false);
                }
            }
            else {
                throw new Exception("No such modal window available for view model type " + typeof(T).FullName);
            }
        }

        public Task<bool> ShowDialog<T>(in T template, out T result) where T : BaseViewModel {
            if (ViewModelToWindow.TryGetValue(typeof(T), out Type windowType)) {
                WindowModal window = (WindowModal) Activator.CreateInstance(windowType);
                window.DataContext = template;
                if (window.ShowDialog() == true && window.DataContext is T t) {
                    result = t;
                    return Task.FromResult(true);
                }
                else {
                    result = null;
                    return Task.FromResult(false);
                }
            }
            else {
                throw new Exception("No such modal window available for view model type " + typeof(T).FullName);
            }
        }
    }
}
