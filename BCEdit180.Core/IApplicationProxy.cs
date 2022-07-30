using System;
using System.Threading.Tasks;

namespace BCEdit180.Core {
    public interface IApplicationProxy {
        void Shutdown();

        void InvokeSync(Action action);
        Task InvokeSyncAsync(Action action);

        void SyspendDispatcherForAction(Action action);
    }
}