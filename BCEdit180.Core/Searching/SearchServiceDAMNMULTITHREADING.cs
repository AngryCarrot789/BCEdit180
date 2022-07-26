using System;
using System.Threading;
using System.Threading.Tasks;
using BCEdit180.Core.Window;

namespace BCEdit180.Core.Searching {
    public class SearchService {
        public delegate void BeginSearchEvent();

        public event BeginSearchEvent BeginSearch;

        private volatile bool stop;

        private volatile int state;

        private int preFireEventState;
        private int fireEventState;

        private DateTime lastBump;

        public TimeSpan MinimumSearchMillis { get; set; }
        public TimeSpan SleepIntervalTime { get; set; }

        private const int STATE_STOPPED = 0;
        private const int STATE_STARTIN = 1;
        private const int STATE_RUNNING = 2;
        private const int STATE_STOPPIN = 3;

        private const int MASK_REQUIRE_START = STATE_STOPPED | STATE_STOPPIN;


        public bool IsStopped => this.state == STATE_STOPPED;
        public bool IsStartin => this.state == STATE_STARTIN;
        public bool IsRunning => this.state == STATE_RUNNING;
        public bool IsStoppin => this.state == STATE_STOPPIN;

        public SearchService() {
            this.preFireEventState = 1;
            this.SleepIntervalTime = TimeSpan.FromMilliseconds(1000);
            this.MinimumSearchMillis = TimeSpan.FromMilliseconds(1000);
        }

        private void Start() {
            Task.Run(async () => {
                Interlocked.Exchange(ref this.state, STATE_RUNNING);
                while (true) {
                    if (this.IsStoppin) {
                        Interlocked.Exchange(ref this.state, STATE_STOPPED);
                        return;
                    }

                    if ((DateTime.Now - this.lastBump).Milliseconds > this.MinimumSearchMillis.Milliseconds) {
                        // Set to 0 | Check if it was 1 (1 means the event can fire)
                        if (Interlocked.CompareExchange(ref this.preFireEventState, 0, 1) == 1) {
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

                    await Task.Delay(this.MinimumSearchMillis);
                }
            });
        }

        public void Stop() {
            Interlocked.Exchange(ref this.state, STATE_STOPPIN);
        }

        public void OnReadySearch() {
            Interlocked.Exchange(ref this.fireEventState, 1);
            ServiceManager.GetService<IApplicationProxy>().InvokeSync(() => {
                if (Interlocked.CompareExchange(ref this.fireEventState, 0, 1) == 1) {
                    this.BeginSearch?.Invoke();
                }
            });
        }

        public void Bump() {
            this.lastBump = DateTime.Now;
            Interlocked.Exchange(ref this.preFireEventState, 1);
            if ((Interlocked.CompareExchange(ref this.state, STATE_STARTIN, STATE_STOPPED) & MASK_REQUIRE_START) != 0) {
                Start();
            }
        }

        public void ForceAction() {
            Interlocked.Exchange(ref this.preFireEventState, 0);
            Interlocked.Exchange(ref this.fireEventState, 0);
            this.lastBump = DateTime.Now;
            OnReadySearch();
        }
    }
}