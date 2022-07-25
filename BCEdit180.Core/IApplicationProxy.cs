using System;

namespace BCEdit180.Core {
    public interface IApplicationProxy {
        void Shutdown();

        void InvokeSync(Action action);
    }
}