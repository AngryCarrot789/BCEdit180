using System.Threading.Tasks;
using BCEdit180.Core.Dialog;

namespace BCEdit180.Core {
    public interface IUIManager {
        void ShowMessage(string title, string message);

        /// <summary>
        /// Shows a dialog using the given input view model, and passes the same view model as an out parameter (for convenience)
        /// </summary>
        /// <returns></returns>
        bool ShowDialog<T>(T input) where T : BaseDialogViewModel;
    }
}