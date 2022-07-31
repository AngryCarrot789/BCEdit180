using System;
using System.Threading;
using System.Threading.Tasks;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class IdleEventService : IDisposable {
        public delegate void BeginActionEvent();

        public event BeginActionEvent OnIdle;

        private DateTime lastInput;

        private volatile bool canFireEvent;

        private volatile bool stopTask;

        public TimeSpan MinimumTimeSinceInput { get; set; }

        public TimeSpan TaskTickInterval { get; set; }

        public bool CanFireNextTick {
            get => this.canFireEvent;
            set => this.canFireEvent = value;
        }

        public IdleEventService() {
            this.MinimumTimeSinceInput = TimeSpan.FromMilliseconds(200);
            this.TaskTickInterval = TimeSpan.FromMilliseconds(100);
            Start();
        }

        private void Start() {
            Task.Run(async () => {
                while (true) {
                    if (this.stopTask) {
                        return;
                    }

                    if ((DateTime.Now - this.lastInput) > this.MinimumTimeSinceInput) {
                        if (this.canFireEvent) {
                            this.canFireEvent = false;
                            try {
                                await ServiceManager.GetService<IApplicationProxy>().DispatchInvokeAsync(FireEvent);
                            }
                            catch (ThreadAbortException) {
                                return;
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

        public void FireEvent() {
            this.OnIdle?.Invoke();
        }

        public void OnInput() {
            if (!this.canFireEvent) {
                this.canFireEvent = true;
            }

            this.lastInput = DateTime.Now;
        }

        public void ForceAction() {
            this.canFireEvent = false;
            this.lastInput = DateTime.Now;
            FireEvent();
        }

        public void Dispose() {
            this.stopTask = true;
        }
    }
}