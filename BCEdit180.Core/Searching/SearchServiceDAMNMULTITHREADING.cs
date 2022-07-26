using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchServiceDAMNMULTITHREADING {
        public delegate void BeginSearchEvent_OLD();

        public event BeginSearchEvent_OLD BeginSearch;

        private DateTime lastBump;

        private volatile bool canFireEvent;

        public TimeSpan MinimumTimeSinceBump { get; set; }

        public TimeSpan TaskTickInterval { get; set; }

        public bool CanFireNextTick {
            get => this.canFireEvent;
            set => this.canFireEvent = value;
        }

        public SearchServiceDAMNMULTITHREADING() {
            this.MinimumTimeSinceBump = TimeSpan.FromMilliseconds(200);
            this.TaskTickInterval = TimeSpan.FromMilliseconds(100);
            Start();
        }

        private void Start() {
            Task.Run(async () => {
                while (true) {
                    // Debug.WriteLine($"Now: {DateTime.Now.ToString("HH:mm:ss.ffff")} -> LastBump: {this.lastBump.ToString("HH:mm:ss.ffff")}");
                    if ((DateTime.Now - this.lastBump) > this.MinimumTimeSinceBump) {
                        if (this.canFireEvent) {
                            this.canFireEvent = false;
                            try {
                                OnReadySearch();
                            }
                            catch (Exception e) {
                                #if DEBUG
                                await Dialog.Message.ShowWarningDialog("Error processing search state", e.ToString());
                                // throw e;
                                #else
                                #endif
                            }
                        }
                    }

                    await Task.Delay(this.TaskTickInterval);
                }
            });
        }

        public void OnReadySearch() {
            ServiceManager.GetService<IApplicationProxy>().InvokeSync(() => {
                this.BeginSearch?.Invoke();
            });
        }

        public void Bump() {
            if (!this.canFireEvent) {
                this.canFireEvent = true;
            }

            this.lastBump = DateTime.Now;
        }

        public void ForceAction() {
            this.canFireEvent = false;
            this.lastBump = DateTime.Now;
            OnReadySearch();
        }
    }
}