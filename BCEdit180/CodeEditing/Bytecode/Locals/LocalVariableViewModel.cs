using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.CodeEditing.Bytecode.Locals {
    public class LocalVariableViewModel : BaseViewModel {
        private ushort startPc;
        private ushort length;
        private string variableName;
        private TypeDescriptor descriptor;
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

        public TypeDescriptor Descriptor {
            get => this.descriptor;
            set => RaisePropertyChanged(ref this.descriptor, value);
        }

        public ushort Index {
            get => this.index;
            set => RaisePropertyChanged(ref this.index, value);
        }
    }
}
