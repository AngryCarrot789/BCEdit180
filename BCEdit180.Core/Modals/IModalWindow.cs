using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.Window {
    public interface IModalWindow<T> where T : BaseViewModel {
        T Model { get; set; }
    }
}