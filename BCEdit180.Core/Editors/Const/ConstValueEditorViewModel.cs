using System;
using System.Windows.Input;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.Instructions.Types;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Editors.Const {
    public class ConstValueEditorViewModel : BaseViewModel {
        private ConstType type;

        public ConstType Type {
            get => this.type;
            set => RaisePropertyChanged(ref this.type, value);
        }

        private int valueInteger;

        public int ValueInteger {
            get => this.valueInteger;
            set => RaisePropertyChanged(ref this.valueInteger, value);
        }

        private long valueLong;

        public long ValueLong {
            get => this.valueLong;
            set => RaisePropertyChanged(ref this.valueLong, value);
        }

        private float valueFloat;

        public float ValueFloat {
            get => this.valueFloat;
            set => RaisePropertyChanged(ref this.valueFloat, value);
        }

        private double valueDouble;

        public double ValueDouble {
            get => this.valueDouble;
            set => RaisePropertyChanged(ref this.valueDouble, value);
        }

        private string valueString;

        public string ValueString {
            get => this.valueString;
            set => RaisePropertyChanged(ref this.valueString, value);
        }

        private ClassName valueClass;

        public ClassName ValueClass {
            get => this.valueClass;
            set => RaisePropertyChanged(ref this.valueClass, value);
        }

        private Handle valueHandle;

        public Handle ValueHandle {
            get => this.valueHandle;
            set => RaisePropertyChanged(ref this.valueHandle, value);
        }

        private MethodDescriptor valueMethodDescriptor;

        public MethodDescriptor ValueMethodDescriptor {
            get => this.valueMethodDescriptor;
            set => RaisePropertyChanged(ref this.valueMethodDescriptor, value);
        }

        public ICommand EditClassNameCommand { get; }

        public ICommand EditMethodDescriptorCommand { get; }

        public ConstValueEditorViewModel() {
            this.EditClassNameCommand = new RelayCommand(EditClassNameAction);
            this.EditMethodDescriptorCommand = new RelayCommand(EditMethodDescriptorAction);
        }

        public ConstValueEditorViewModel(object defaultValue) : this() {
            if (defaultValue is byte || defaultValue is sbyte || defaultValue is ushort || defaultValue is short || defaultValue is uint || defaultValue is int) {
                this.Type = ConstType.Integer;
                this.ValueInteger = (int) defaultValue;
            }
            else if (defaultValue is long || defaultValue is ulong) {
                this.Type = ConstType.Long;
                this.ValueLong = (long) defaultValue;
            }
            else if (defaultValue is float) {
                this.Type = ConstType.Float;
                this.ValueFloat = (float) defaultValue;
            }
            else if (defaultValue is double) {
                this.Type = ConstType.Double;
                this.ValueDouble = (double) defaultValue;
            }
            else if (defaultValue is string) {
                this.Type = ConstType.String;
                this.ValueString = (string) defaultValue;
            }
            else if (defaultValue is ClassName) {
                this.Type = ConstType.Class;
                this.ValueClass = (ClassName) defaultValue;
            }
            else if (defaultValue is Handle) {
                this.Type = ConstType.Handle;
                this.ValueHandle = (Handle) defaultValue;
            }
            else if (defaultValue is MethodDescriptor) {
                this.Type = ConstType.MethodDescriptor;
                this.ValueMethodDescriptor = (MethodDescriptor) defaultValue;
            }
            else {
                throw new ArgumentException("Unknown type: " + defaultValue, nameof(defaultValue));
            }
        }

        public object GetValue() {
            switch (this.Type) {
                case ConstType.Integer:          return this.ValueInteger;
                case ConstType.Long:             return this.ValueLong;
                case ConstType.Float:            return this.ValueFloat;
                case ConstType.Double:           return this.ValueDouble;
                case ConstType.String:           return this.ValueString;
                case ConstType.Class:            return this.ValueClass;
                case ConstType.Handle:           return this.ValueHandle;
                case ConstType.MethodDescriptor: return this.ValueMethodDescriptor;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void EditClassNameAction() {
            if (Dialog.TypeEditor.EditTypeDescriptorDialog(new TypeDescriptor(this.ValueClass ?? new ClassName(""), 0), out TypeDescriptor descriptor, true, false).Result) {
                this.ValueClass = descriptor.ClassName;
            }
        }

        public void EditMethodDescriptorAction() {
            if (Dialog.TypeEditor.EditMethodDescriptorDialog(this.ValueMethodDescriptor, out MethodDescriptor typeDesc).Result) {
                this.ValueMethodDescriptor = typeDesc;
            }
        }
    }
}