using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BCEdit180.Core.Searching;
using BCEdit180.Core.Window;
using JavaAsm.Instructions;

namespace BCEdit180.Core.CodeEditing.InstructionEdit {
    public class OpcodeSearchViewModel : SearchViewModel {
        public ChangeInstructionViewModel ChangeInstruction { get; }

        public OpcodeSearchViewModel(ChangeInstructionViewModel changeInstruction) {
            this.ChangeInstruction = changeInstruction;
            this.IdleEventService.OnIdle += FindNextInstruction;
            this.IdleEventService.MinimumTimeSinceInput = TimeSpan.FromMilliseconds(200);
        }

        ~OpcodeSearchViewModel() {
            // is this even necessary though?
            this.IdleEventService.OnIdle -= FindNextInstruction;
        }

        public void FindNextInstruction() {
            if (!CanSearchForInput()) {
                return;
            }

            bool is2nd = false;
            string search = this.InputText.ToLower();
            ObservableCollection<Opcode> opcodes = this.ChangeInstruction.AvailableOpCodes;
            List<Opcode> matches = new List<Opcode>();
            while (true) {
                int start = is2nd ? 0 : (this.ChangeInstruction.SelectedIndex + 1);
                if (start < 0 || start >= opcodes.Count) {
                    if (is2nd) {
                        break;
                    }

                    start = 0;
                }

                for (int i = start, size = opcodes.Count; i < size; i++) {
                    if (MatchOpcode(opcodes[i], search)) {
                        matches.Add(opcodes[i]);
                    }
                }

                if (matches.Count > 0) {
                    this.ChangeInstruction.ActualOpcodeList.Clear();
                    this.ChangeInstruction.ActualOpcodeList.AddRange(matches);
                    this.ChangeInstruction.SelectedIndex = 0;
                    return;
                }

                if (!is2nd) {
                    is2nd = true;
                }
                else {
                    if (this.WasLastSearchForced) {
                        Dialog.Message.ShowInformationDialog("Nothing found", "No search results found for: " + search);
                    }

                    break;
                }
            }
        }

        private bool MatchOpcode(Opcode opcode, string search) {
            return opcode.ToString().ToLower().Contains(search);
        }

        public override void OnSearchReset() {
            base.OnSearchReset();
            this.ChangeInstruction.ActualOpcodeList.Clear();
            this.ChangeInstruction.ActualOpcodeList.AddRange(this.ChangeInstruction.AvailableOpCodes);
            this.ChangeInstruction.SelectedIndex = 0;
        }
    }
}