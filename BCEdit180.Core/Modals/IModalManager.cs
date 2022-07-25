using System.Threading.Tasks;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Modals {
    public interface IModalManager {
        Task<bool> ShowDialog<T>(out T result) where T : BaseViewModel, new();

        Task<bool> ShowDialog<T>(in T template, out T result) where T : BaseViewModel;
    }
}