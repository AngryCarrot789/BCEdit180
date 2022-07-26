using System;
using System.Windows;
using System.Windows.Threading;
using BCEdit180.Core;

namespace BCEdit180 {
    public class WpfApplicationProxy : IApplicationProxy {
        public void Shutdown() {
            Application.Current.Shutdown();
        }

        public void InvokeSync(Action action) {
            Application.Current.Dispatcher.Invoke(action);
        }

        public void SyspendDispatcherForAction(Action action) {
            using (DispatcherProcessingDisabled disabled = Application.Current.Dispatcher.DisableProcessing()) {
                action();
            }
        }
    }
}