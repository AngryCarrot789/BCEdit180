using System;
using System.Threading;
using System.Windows.Input;

namespace BCEdit180.Core.Commands {
    public class ExtendedRelayCommand : ICommand {
        private readonly Action action;
        private readonly Func<bool> canExecute;

        private EventHandler requerySuggestedLocal;

        public event EventHandler CanExecuteChanged {
            add {
                if (this.canExecute == null)
                    return;
                EventHandler eventHandler = this.requerySuggestedLocal;
                EventHandler comparand;
                do {
                    comparand = eventHandler;
                    eventHandler = Interlocked.CompareExchange(ref this.requerySuggestedLocal, comparand + value, comparand);
                } while (eventHandler != comparand);

                ServiceManager.GetService<ICommandManager>().AddRequerySuggestionHandler(value);
            }
            remove {
                if (this.canExecute == null)
                    return;
                EventHandler eventHandler = this.requerySuggestedLocal;
                EventHandler comparand;
                do {
                    comparand = eventHandler;
                    eventHandler = Interlocked.CompareExchange(ref this.requerySuggestedLocal, comparand - value, comparand);
                } while (eventHandler != comparand);

                ServiceManager.GetService<ICommandManager>().RemoveRequerySuggestionHandler(value);
            }
        }

        public ExtendedRelayCommand(Action action, Func<bool> canExecute) {
            this.action = action ?? throw new ArgumentNullException(nameof(action), "Action cannot be null");
            this.canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged() {
            ServiceManager.GetService<ICommandManager>().InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter) {
            return this.canExecute == null || this.canExecute();
        }

        public void Execute(object parameter) {
            this.action();
        }
    }
}