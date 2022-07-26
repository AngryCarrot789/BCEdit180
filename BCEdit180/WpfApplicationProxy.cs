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