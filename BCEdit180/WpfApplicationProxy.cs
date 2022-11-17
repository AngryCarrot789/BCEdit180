using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BCEdit180.Core;

namespace BCEdit180 {
    public class WpfApplicationProxy : IApplicationProxy {
        public void Shutdown() {
            Application.Current.Shutdown();
        }

        public bool IsRunning() {
            try {
                Application app = Application.Current;
                if (app != null) {
                    app.ShutdownMode = app.ShutdownMode;
                }

                return false;
            }
            catch (Exception) {
                return true;
            }
        }

        /// <summary>
        /// Runs the given action on the main thread. This method may return before the action is complete
        /// </summary>
        public void DispatchInvoke(Action action) {
            Application app = Application.Current;
            if (app != null) {
                app.Dispatcher.Invoke(action);
            }
            else {
                Console.WriteLine("Warning! Failed to dispatch action to main thread. Is the app shutting down?");
                Console.WriteLine(new Exception().ToString());
            }
        }

        /// <summary>
        /// Runs the given action on the main thread, and waits for it to complete.
        /// This method will never return before the action is complete
        /// </summary>
        /// <param name="action"></param>
        public async Task DispatchInvokeAsync(Action action) {
            int state = 0;
            Exception error = null;
            Application.Current.Dispatcher.Invoke(() => {
                try {
                    action();
                }
                catch (Exception e) {
                    error = e;
                }
                finally {
                    state = 1;
                }
            });

            do {
                await Task.Delay(1);
            } while (state == 0);

            if (error != null) {
                throw error;
            }
        }

        // was checking if this would improve performance while adding items...
        // this was before i realised that there's no way to create like
        // 200+ ListBoxItems (for the bytecode editor) in a split second
        // due to how complex the control hierarchy is + data binding + arrangement
        public void SyspendDispatcherForAction(Action action) {
            using (DispatcherProcessingDisabled disabled = Application.Current.Dispatcher.DisableProcessing()) {
                action();
            }
        }
    }
}