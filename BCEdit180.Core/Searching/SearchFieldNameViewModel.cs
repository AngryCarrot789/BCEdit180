using System;
using System.Collections.ObjectModel;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchFieldNameViewModel : TimedInputUpdate {
        public FieldListViewModel FieldList { get; }

        public SearchFieldNameViewModel(FieldListViewModel fieldList) {
            this.FieldList = fieldList;
            this.IdleEventService.OnIdle += FindNextMethod;
            this.IdleEventService.MinimumTimeSinceInput = TimeSpan.FromMilliseconds(200);
        }

        ~SearchFieldNameViewModel() {
            // is this even necessary though?
            this.IdleEventService.OnIdle -= FindNextMethod;
        }

        public void FindNextMethod() {
            // Debug.WriteLine("Begun method search for term: " + this.InputText);
            if (!CanSearchForInput()) {
                return;
            }

            bool is2nd = false;
            string search = this.InputText.ToLower();
            ObservableCollection<FieldInfoViewModel> methods = this.FieldList.Fields;
            while (true) {
                int start = is2nd ? 0 : (this.FieldList.SelectedIndex + 1);
                if (start < 0 || start >= methods.Count) {
                    if (is2nd) {
                        break;
                    }

                    start = 0;
                }

                for (int i = start, size = methods.Count; i < size; i++) {
                    if (MatchField(methods[i], search)) {
                        this.FieldList.SelectedIndex = i;
                        FieldListViewModel.FieldList.ScrollToSelectedItem();
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

        private bool MatchField(FieldInfoViewModel method, string search) {
            return method.FieldName.ToLower().Contains(search);
        }
    }
}