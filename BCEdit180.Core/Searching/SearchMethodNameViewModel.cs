using System;
using System.Collections.ObjectModel;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchMethodNameViewModel : SearchViewModel {
        public MethodListViewModel MethodList { get; }

        public SearchMethodNameViewModel(MethodListViewModel methodList) {
            this.MethodList = methodList;
            this.SearchService.SearchReady += FindNextMethod;
            this.SearchService.MinimumTimeSinceBump = TimeSpan.FromMilliseconds(200);
        }

        ~SearchMethodNameViewModel() {
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
            ObservableCollection<MethodInfoViewModel> methods = this.MethodList.Methods;
            while (true) {
                int start = is2nd ? 0 : (this.MethodList.SelectedIndex + 1);
                if (start < 0 || start >= methods.Count) {
                    if (is2nd) {
                        break;
                    }

                    start = 0;
                }

                for (int i = start, size = methods.Count; i < size; i++) {
                    if (MatchMethod(methods[i], search)) {
                        this.MethodList.SelectedIndex = i;
                        MethodListViewModel.MethodList.ScrollToSelectedItem();
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

        private bool MatchMethod(MethodInfoViewModel method, string search) {
            return method.MethodName.ToLower().Contains(search);
        }
    }
}