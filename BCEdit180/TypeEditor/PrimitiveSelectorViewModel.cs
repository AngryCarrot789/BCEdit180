using REghZy.MVVM.ViewModels;

namespace BCEdit180.TypeEditor {
    public class PrimitiveSelectorViewModel : BaseViewModel {
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

        public bool SelectedByte { get => this.selectedByte; set => RaisePropertyChangedCheckEqual(ref this.selectedByte, value); }
        public bool SelectedShort { get => this.selectedShort; set => RaisePropertyChangedCheckEqual(ref this.selectedShort, value); }
        public bool SelectedInt { get => this.selectedInt; set => RaisePropertyChangedCheckEqual(ref this.selectedInt, value); }
        public bool SelectedLong { get => this.selectedLong; set => RaisePropertyChangedCheckEqual(ref this.selectedLong, value); }
        public bool SelectedFloat { get => this.selectedFloat; set => RaisePropertyChangedCheckEqual(ref this.selectedFloat, value); }
        public bool SelectedDouble { get => this.selectedDouble; set => RaisePropertyChangedCheckEqual(ref this.selectedDouble, value); }
        public bool SelectedChar { get => this.selectedChar; set => RaisePropertyChangedCheckEqual(ref this.selectedChar, value); }
        public bool SelectedBool { get => this.selectedBool; set => RaisePropertyChangedCheckEqual(ref this.selectedBool, value); }
        public bool SelectedVoid { get => this.selectedVoid; set => RaisePropertyChangedCheckEqual(ref this.selectedVoid, value); }
        public bool SelectedObject { get => this.selectedObject; set => RaisePropertyChangedCheckEqual(ref this.selectedObject, value); }

        public void SetAll(bool state) {
            this.SelectedByte = state;
            this.SelectedShort = state;
            this.SelectedInt = state;
            this.SelectedLong = state;
            this.SelectedFloat = state;
            this.SelectedDouble = state;
            this.SelectedChar = state;
            this.SelectedBool = state;
            this.SelectedVoid = state;
            this.SelectedObject = state;
        }

        public void Set(bool primitives, bool objects) {
            this.SelectedByte = primitives;
            this.SelectedShort = primitives;
            this.SelectedInt = primitives;
            this.SelectedLong = primitives;
            this.SelectedFloat = primitives;
            this.SelectedDouble = primitives;
            this.SelectedChar = primitives;
            this.SelectedBool = primitives;
            this.SelectedVoid = primitives;
            this.SelectedObject = objects;
        }
    }
}
