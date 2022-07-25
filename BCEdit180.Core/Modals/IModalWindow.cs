using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Modals {
    public interface IModalWindow<T> where T : BaseViewModel {
        T Model { get; set; }
    }
}