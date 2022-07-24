using System;

namespace BCEdit180.Core {
    public interface AppServices {
        public static Action Shutdown { get; set; }

        public static void RunSync(Action action) {

        }
    }
}