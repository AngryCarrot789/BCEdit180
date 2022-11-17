using System;
using System.Threading.Tasks;

namespace BCEdit180.Core {
    public interface IApplicationProxy {
        void Shutdown();

        void DispatchInvoke(Action action);

        Task DispatchInvokeAsync(Action action);

        void SyspendDispatcherForAction(Action action);

        bool IsRunning();
    }
}