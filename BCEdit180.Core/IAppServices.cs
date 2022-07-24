using System;

namespace BCEdit180.Core {
    public interface IAppServices {
        void Shutdown();

        void RunSync(Action action);
    }
}