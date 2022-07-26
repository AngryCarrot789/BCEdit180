using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchFieldNameViewModel : SearchViewModel {
        public FieldListViewModel FieldList { get; }

        public SearchFieldNameViewModel(FieldListViewModel fieldList) {
            this.FieldList = fieldList;
            this.SearchService.SearchReady += FindNextMethod;
            this.SearchService.MinimumTimeSinceBump = TimeSpan.FromMilliseconds(200);
        }

        ~SearchFieldNameViewModel() {
            // is this even necessary though?
            this.SearchService.SearchReady -= FindNextMethod;
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
                        Dialog.Message.ShowInformationDialog("Nothing found", "No search results found for: " + search);
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