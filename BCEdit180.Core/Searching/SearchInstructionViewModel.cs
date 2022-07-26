using System;
using System.Collections.ObjectModel;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchInstructionViewModel : TimedInputUpdate {
        public BytecodeEditorViewModel Editor { get; }

        public SearchInstructionViewModel(BytecodeEditorViewModel editor) {
            this.Editor = editor;
            this.IdleEventService.OnIdle += FindNextInstruction;
            this.IdleEventService.MinimumTimeSinceInput = TimeSpan.FromMilliseconds(200);
        }

        public void FindNextInstruction() {
            // Debug.WriteLine("Begun method search for term: " + this.InputText);
            if (!CanSearchForInput()) {
                return;
            }

            bool is2nd = false;
            string search = this.InputText.ToLower();
            ObservableCollection<BaseInstructionViewModel> methods = this.Editor.Instructions;
            while (true) {
                int start = is2nd ? 0 : (this.Editor.SelectedInstructionIndex + 1);
                if (start < 0 || start >= methods.Count) {
                    if (is2nd) {
                        break;
                    }

                    start = 0;
                }

                for (int i = start, size = methods.Count; i < size; i++) {
                    if (MatchInstruction(methods[i], search)) {
                        this.Editor.SelectAndScrollToInstruction(i);
                        return;
                    }
                }


                if (!is2nd) {
                    is2nd = true;
                }
                else {
                    if (this.WasLastSearchForced) {
                        Dialogs.Message.ShowMessage("Nothing found", "No search results found for: " + search);
                    }

                    break;
                }
            }
        }

        public static bool MatchInstruction(BaseInstructionViewModel instruction, string search) {
            string tostr = instruction.Instruction.ToString();
            return tostr.ToLower().Contains(search.ToLower());
        }
    }
}