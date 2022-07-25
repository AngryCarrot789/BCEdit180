using System;
using System.Windows.Input;
using BCEdit180.Core.Commands;

namespace BCEdit180 {
    public class WPFCommandManager : ICommandManager {
        public void AddRequerySuggestionHandler(EventHandler handler) {
            CommandManager.RequerySuggested += handler;
        }

        public void RemoveRequerySuggestionHandler(EventHandler handler) {
            CommandManager.RequerySuggested -= handler;
        }

        public void InvalidateRequerySuggested() {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}