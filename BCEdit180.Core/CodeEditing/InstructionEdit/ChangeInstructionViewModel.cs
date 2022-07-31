using System;
using System.Collections.Generic;
using BCEdit180.Core.Collections;
using JavaAsm.Instructions;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.InstructionEdit {
    public class ChangeInstructionViewModel : BaseViewModel, IDisposable {
        public ExtendedObservableCollection<Opcode> AvailableOpCodes { get; }

        public ExtendedObservableCollection<Opcode> ActualOpcodeList { get; }

        public OpcodeSearchViewModel Search { get; }

        private int selectedIndex;
        public int SelectedIndex{
            get => this.selectedIndex;
            set => RaisePropertyChanged(ref this.selectedIndex, value);
        }

        private Opcode selectedOpcode;
        public Opcode SelectedOpcode {
            get => this.selectedOpcode;
            set => RaisePropertyChanged(ref this.selectedOpcode, value);
        }

        public ChangeInstructionViewModel() {
            this.AvailableOpCodes = new ExtendedObservableCollection<Opcode>();
            this.ActualOpcodeList = new ExtendedObservableCollection<Opcode>();
            this.Search = new OpcodeSearchViewModel(this);
        }

        public void SetAvailableInstructions(IEnumerable<Opcode> opcodes) {
            this.AvailableOpCodes.Clear();
            this.ActualOpcodeList.Clear();
            this.AvailableOpCodes.AddRange(opcodes);
            this.ActualOpcodeList.AddRange(this.AvailableOpCodes);
        }

        public bool IsValidSelection() {
            return this.selectedIndex >= 0 && this.selectedIndex < this.AvailableOpCodes.Count;
        }

        public void Dispose() {
            this.Search.Dispose();
        }
    }
}
