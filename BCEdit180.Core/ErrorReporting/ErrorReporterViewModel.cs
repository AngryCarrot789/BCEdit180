using System;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages.ErrorReporting;
using BCEdit180.Core.ViewModels;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ErrorReporting {
    public class ErrorReporterViewModel : BaseViewModel, IDisposable, IMessageReceiver<CheckField> {
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

        public ClassViewModel Class { get; }

        public ErrorReporterViewModel(ClassViewModel classViewModel) {
            this.Class = classViewModel;
            MessageDispatcher.RegisterHandler<CheckField>(this);
        }

        public void Dispose() {
            MessageDispatcher.UnregisterHandler<CheckField>(this);
        }

        public void CheckFieldState(FieldInfoViewModel field) {
            Reset();
            if (field.ConstantValue == null) {
                return;
            }
            else if (field.Descriptor.PrimitiveType.HasValue) {
                switch (field.Descriptor.PrimitiveType.Value) {
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
            else if (field.Descriptor.ClassName != null && field.Descriptor.ClassName.Name == "java/lang/String") {
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
    }
}