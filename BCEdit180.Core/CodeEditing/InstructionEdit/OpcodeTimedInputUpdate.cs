using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Window;
using JavaAsm.Instructions;

namespace BCEdit180.Core.CodeEditing.InstructionEdit {
    public class OpcodeTimedInputUpdate : TimedInputUpdate {
        public ChangeInstructionViewModel ChangeInstruction { get; }

        public OpcodeTimedInputUpdate(ChangeInstructionViewModel changeInstruction) {
            this.ChangeInstruction = changeInstruction;
            this.IdleEventService.OnIdle += FindNextInstruction;
            this.IdleEventService.MinimumTimeSinceInput = TimeSpan.FromMilliseconds(200);
        }

        ~OpcodeTimedInputUpdate() {
            // is this even necessary though?
            this.IdleEventService.OnIdle -= FindNextInstruction;
        }

        public void FindNextInstruction() {
            if (!CanSearchForInput()) {
                return;
            }

            string search = this.InputText.ToLower();
            int intSearch = int.TryParse(search, out int value) ? value : -1;
            List<Opcode> matches = new List<Opcode>();
            foreach (Opcode opcode in this.ChangeInstruction.AvailableOpCodes) {
                if (MatchOpcode(opcode, search, intSearch)) {
                    matches.Add(opcode);
                }
            }

            if (matches.Count > 0) {
                this.ChangeInstruction.ActualOpcodeList.Clear();
                this.ChangeInstruction.ActualOpcodeList.AddRange(matches);
                this.ChangeInstruction.SelectedIndex = 0;
                return;
            }

            if (this.WasLastSearchForced) {
                Dialogs.Message.ShowMessage("Nothing found", "No search results found for: " + search);
            }
        }

        private bool MatchOpcode(Opcode opcode, string search, int intSearch) {
            return (int) opcode == intSearch || opcode.ToString().ToLower().Contains(search);
        }

        public override void OnInputReset() {
            base.OnInputReset();
            this.ChangeInstruction.ActualOpcodeList.Clear();
            this.ChangeInstruction.ActualOpcodeList.AddRange(this.ChangeInstruction.AvailableOpCodes);
            this.ChangeInstruction.SelectedIndex = 0;
        }
    }
}