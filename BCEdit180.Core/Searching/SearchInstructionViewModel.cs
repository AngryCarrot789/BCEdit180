using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchInstructionViewModel : SearchViewModel {
        public BytecodeEditorViewModel Editor { get; }

        public SearchInstructionViewModel(BytecodeEditorViewModel editor) {
            this.Editor = editor;
            this.SearchService.SearchReady += FindNextInstruction;
            this.SearchService.MinimumTimeSinceBump = TimeSpan.FromMilliseconds(200);
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
                        this.Editor.SelectedInstructionIndex = i;
                        BytecodeEditorViewModel.BytecodeList.ScrollToSelectedItem();
                        return;
                    }
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

        public static bool MatchInstruction(BaseInstructionViewModel instruction, string search) {
            string tostr = instruction.Node.ToString();
            return tostr.ToLower().Contains(search.ToLower());
        }
    }
}