using System;
using System.Windows;
using BCEdit180.Core;

namespace BCEdit180 {
    public class WPFAppServices : IAppServices {
        public void Shutdown() {
            Application.Current.Shutdown();
        }

        public void RunSync(Action action) {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}