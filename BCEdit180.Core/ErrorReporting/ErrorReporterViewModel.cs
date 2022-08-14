using System;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages;
using BCEdit180.Core.Messaging.Messages.ErrorReporting;
using BCEdit180.Core.ViewModels;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ErrorReporting {
    public class ErrorReporterViewModel : BaseViewModel, IDisposable, IMessageReceiver<CheckField>, IMessageReceiver<AddMessage> {
        private string simpleDescription;
        public string SimpleDescription {
            get => this.simpleDescription;
            set => RaisePropertyChanged(ref this.simpleDescription, value);
        }

        private bool isWarning;
        public bool IsWarning {
            get => this.isWarning;
            set => RaisePropertyChanged(ref this.isWarning, value);
        }

        public ErrorReporterViewModel(ClassListViewModel list) {
            MessageDispatcher.RegisterHandler<CheckField>(this);
            MessageDispatcher.RegisterHandler<AddMessage>(this);
        }

        public void Dispose() {
            MessageDispatcher.UnregisterHandler<CheckField>(this);
            MessageDispatcher.UnregisterHandler<AddMessage>(this);
        }

        public void CheckFieldState(FieldInfoViewModel field) {
            Reset();
            if (field.ConstantValue == null) {
                return;
            }
            else if (field.FieldDescriptor.PrimitiveType.HasValue) {
                switch (field.FieldDescriptor.PrimitiveType.Value) {
                    case PrimitiveType.Integer:
                        if (!(field.ConstantValue is int))
                            SetError($"Field {field.FieldName} has a constant {field.ConstantValue.GetType().Name} value of {field.ConstantValue}, but it's descriptor requires integer");
                        break;
                    case PrimitiveType.Long:
                        if (!(field.ConstantValue is long))
                            SetError($"Field {field.FieldName} has a constant {field.ConstantValue.GetType().Name} value of {field.ConstantValue}, but it's descriptor requires long");
                        break;
                    case PrimitiveType.Float:
                        if (!(field.ConstantValue is float))
                            SetError($"Field {field.FieldName} has a constant {field.ConstantValue.GetType().Name} value of {field.ConstantValue}, but it's descriptor requires float");
                        break;
                    case PrimitiveType.Double:
                        if (!(field.ConstantValue is double))
                            SetError($"Field {field.FieldName} has a constant {field.ConstantValue.GetType().Name} value of {field.ConstantValue}, but it's descriptor requires double");
                        break;
                    default: return;
                }
            }
            else if (field.FieldDescriptor.ClassName != null && field.FieldDescriptor.ClassName.Name == "java/lang/String") {
                if (!(field.ConstantValue is string)) {
                    SetError($"Field {field.FieldName} has a constant {field.ConstantValue.GetType().Name} value of {field.ConstantValue}, but it's descriptor requires a string");
                }
            }
        }

        public void SetWarning(string text) {
            this.IsWarning = true;
            this.SimpleDescription = text;
        }

        public void SetError(string text) {
            this.IsWarning = false;
            this.SimpleDescription = text;
        }

        public void Reset() {
            this.IsWarning = false;
            this.SimpleDescription = null;
        }

        public void HandleMessage(CheckField message) {
            if (message.Field != null) {
                CheckFieldState(message.Field);
            }
            else {
                throw new ArgumentException("Message's field was null");
            }
        }

        public void HandleMessage(AddMessage message) {
            this.SetWarning(message.Message);
        }
    }
}