using System.Threading.Tasks;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Window {
    public interface IModalManager {
        Task<bool> ShowDialog<T>(out T result) where T : BaseViewModel;

        Task<bool> ShowDialog<T>(in T template, out T result) where T : BaseViewModel;
    }
}