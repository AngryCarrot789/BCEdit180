using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaAsm;
using REghZy.MVVM.Commands;
using System.Windows.Input;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Descriptors {
    public class MethodDescriptorEditorViewModel : BaseViewModel {
        private string signature;

        public string Signature {
            get => this.signature;
            set => RaisePropertyChanged(ref this.signature, value);
        }

        private string className;
        public string ClassName {
            get => this.className;
            set => RaisePropertyChanged(ref this.className, value);
        }

        public int ArrayDepth {
            get => this.arrayDepth;
            set => RaisePropertyChanged(ref this.arrayDepth, value);
        }

        private bool isCheckedByte;
        private bool isCheckedShort;
        private bool isCheckedInteger;
        private bool isCheckedLong;
        private bool isCheckedFloat;
        private bool isCheckedDouble;
        private bool isCheckedCharacter;
        private bool isCheckedBoolean;
        private bool isCheckedVoid;
        private bool isCheckedObject;
        private int arrayDepth;

        public bool IsCheckedByte {
            get => this.isCheckedByte;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedByte, value);
                UpdateState();
            }
        }

        public bool IsCheckedShort {
            get => this.isCheckedShort;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedShort, value);
                UpdateState();
            }
        }

        public bool IsCheckedInteger {
            get => this.isCheckedInteger;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedInteger, value);
                UpdateState();
            }
        }

        public bool IsCheckedLong {
            get => this.isCheckedLong;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedLong, value);
                UpdateState();
            }
        }

        public bool IsCheckedFloat {
            get => this.isCheckedFloat;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedFloat, value);
                UpdateState();
            }
        }

        public bool IsCheckedDouble {
            get => this.isCheckedDouble;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedDouble, value);
                UpdateState();
            }
        }

        public bool IsCheckedCharacter {
            get => this.isCheckedCharacter;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedCharacter, value);
                UpdateState();
            }
        }

        public bool IsCheckedBoolean {
            get => this.isCheckedBoolean;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedBoolean, value);
                UpdateState();
            }
        }

        public bool IsCheckedVoid {
            get => this.isCheckedVoid;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedVoid, value);
                UpdateState();
            }
        }

        public bool IsCheckedObject {
            get => this.isCheckedObject;
            set {
                RaisePropertyChangedCheckEqual(ref this.isCheckedObject, value);
                UpdateState();
            }
        }

        private void UpdateState() {

        }

        public Action<string, PrimitiveType?, int> Callback { get; set; }

        public ICommand ApplyChanges { get; }

        public MethodDescriptorEditorViewModel() {
            this.ApplyChanges = new RelayCommand(ApplyChange);
        }

        public void ApplyChange() {
            if (this.isCheckedByte) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedShort) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedInteger) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedLong) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedFloat) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedDouble) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedCharacter) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedBoolean) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedVoid) {
                this.Callback?.Invoke(this.ClassName, PrimitiveType.Byte, this.ArrayDepth);
            }
            else if (this.isCheckedObject) {
                this.Callback?.Invoke(this.ClassName, null, this.ArrayDepth);
            }
            else {
                throw new Exception("????????");
            }
        }

        public void SetPrimitiveType(PrimitiveType? type) {
            switch (type) {
                case PrimitiveType.Boolean: this.IsCheckedBoolean = true; break;
                case PrimitiveType.Byte: this.IsCheckedByte = true; break;
                case PrimitiveType.Character: this.IsCheckedCharacter = true; break;
                case PrimitiveType.Double: this.IsCheckedDouble = true; break;
                case PrimitiveType.Float: this.IsCheckedFloat = true; break;
                case PrimitiveType.Integer: this.IsCheckedInteger = true; break;
                case PrimitiveType.Long: this.IsCheckedLong = true; break;
                case PrimitiveType.Short: this.IsCheckedShort = true; break;
                case PrimitiveType.Void: this.IsCheckedVoid = true; break;
                case null: this.IsCheckedObject = true; break;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
