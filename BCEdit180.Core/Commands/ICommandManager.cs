using System;

namespace BCEdit180.Core.Commands {
    public interface ICommandManager {
        void AddRequerySuggestionHandler(EventHandler handler);
        void RemoveRequerySuggestionHandler(EventHandler handler);

        void InvalidateRequerySuggested();
    }
}