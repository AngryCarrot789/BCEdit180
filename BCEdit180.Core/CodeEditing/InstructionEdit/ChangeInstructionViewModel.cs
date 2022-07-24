using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BCEdit180.Core.Utils;
using JavaAsm.Instructions;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.CodeEditing.InstructionEdit {
    public class ChangeInstructionViewModel : BaseViewModel {
        public ObservableCollection<Opcode> AvailableOpCodes { get; }

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

        public ICommand ApplyChangesCommand { get; }

        public Action<Opcode> SetOpcodeCallback { get; set; }

        public ChangeInstructionViewModel() {
            this.ApplyChangesCommand = new RelayCommand(ApplyChanges);
            this.AvailableOpCodes = new ObservableCollection<Opcode>();
        }

        public void SetAvailableInstructions(IEnumerable<Opcode> opcodes) {
            this.AvailableOpCodes.Clear();
            this.AvailableOpCodes.AddAll(opcodes);
        }

        public bool IsValidSelection() {
            return this.selectedIndex >= 0 && this.selectedIndex < this.AvailableOpCodes.Count;
        }

        public void ApplyChanges() {
            if (IsValidSelection()) {
                this.SetOpcodeCallback?.Invoke(this.SelectedOpcode);
            }
        }
    }
}
