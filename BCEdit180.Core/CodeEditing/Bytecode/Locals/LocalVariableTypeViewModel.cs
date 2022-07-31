using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.Bytecode.Locals {
    public class LocalVariableTypeViewModel : BaseViewModel {
        private ushort startPc;
        private ushort length;
        private string variableName;
        private string signature;
        private ushort index;

        public ushort StartPC {
            get => this.startPc;
            set => RaisePropertyChanged(ref this.startPc, value);
        }

        public ushort Length {
            get => this.length;
            set => RaisePropertyChanged(ref this.length, value);
        }

        public string VariableName {
            get => this.variableName;
            set => RaisePropertyChanged(ref this.variableName, value);
        }

        public string Signature {
            get => this.signature;
            set => RaisePropertyChanged(ref this.signature, value);
        }

        public ushort Index {
            get => this.index;
            set => RaisePropertyChanged(ref this.index, value);
        }
    }
}
