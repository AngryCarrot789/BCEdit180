using System;
using System.Threading;
using System.Windows.Input;

namespace BCEdit180.Core.Commands {
    public abstract class BaseRelayCommand : ICommand {
        private EventHandler requerySuggestedLocal;

        public event EventHandler CanExecuteChanged {
            add {
                if (this.HasExecutionTrigger) {
                    EventHandler eventHandler = this.requerySuggestedLocal;
                    EventHandler comparand;
                    do {
                        comparand = eventHandler;
                        eventHandler = Interlocked.CompareExchange(ref this.requerySuggestedLocal, comparand + value, comparand);
                    } while (eventHandler != comparand);

                    ServiceManager.GetService<ICommandManager>().AddRequerySuggestionHandler(value);
                }
            }
            remove {
                if (this.HasExecutionTrigger) {
                    EventHandler eventHandler = this.requerySuggestedLocal;
                    EventHandler comparand;
                    do {
                        comparand = eventHandler;
                        eventHandler = Interlocked.CompareExchange(ref this.requerySuggestedLocal, comparand - value, comparand);
                    } while (eventHandler != comparand);

                    ServiceManager.GetService<ICommandManager>().RemoveRequerySuggestionHandler(value);
                }
            }
        }

        protected abstract bool HasExecutionTrigger { get; }

        public void RaiseCanExecuteChanged() {
            ServiceManager.GetService<ICommandManager>().InvalidateRequerySuggested();
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }

    public class ExtendedRelayCommand : BaseRelayCommand {
        private readonly Action action;
        private readonly Func<bool> canExecute;

        protected override bool HasExecutionTrigger => this.canExecute != null;

        public ExtendedRelayCommand(Action action, Func<bool> canExecute) {
            this.action = action ?? throw new ArgumentNullException(nameof(action), "Action cannot be null");
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) {
            return this.canExecute == null || this.canExecute();
        }

        public override void Execute(object parameter) {
            this.action();
        }
    }

    public class ExtendedRelayCommand<T> : BaseRelayCommand {
        private readonly Type type;
        private readonly Action<T> action;
        private readonly Predicate<T> canExecute;

        protected override bool HasExecutionTrigger => this.canExecute != null;

        public ExtendedRelayCommand(Action<T> action, Predicate<T> canExecute) {
            this.type = typeof(T);
            this.action = action ?? throw new ArgumentNullException(nameof(action), "Action cannot be null");
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) {
            return this.canExecute == null || this.canExecute(GetCompatibleValueFor(parameter));
        }

        public override void Execute(object parameter) {
            this.action(GetCompatibleValueFor(parameter));
        }

        private T GetCompatibleValueFor(object value) {
            return value == null || value is T ? (T) value : throw new Exception($"Incompatible parameter: {value.GetType()} cannot be converted to {this.type}");
        }
    }
}