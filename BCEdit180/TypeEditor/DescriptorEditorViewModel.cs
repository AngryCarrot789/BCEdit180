using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaAsm;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.TypeEditor {
    public class DescriptorEditorViewModel : BaseViewModel {
        public TypeDescriptor Descriptor { get; }

        private bool selectedByte;
        private bool selectedShort;
        private bool selectedInt;
        private bool selectedLong;
        private bool selectedFloat;
        private bool selectedDouble;
        private bool selectedChar;
        private bool selectedBool;
        private bool selectedVoid;
        private bool selectedObject;

        public DescriptorEditorViewModel(TypeDescriptor descriptor) {
            this.Descriptor = descriptor;
        }
    }
}
