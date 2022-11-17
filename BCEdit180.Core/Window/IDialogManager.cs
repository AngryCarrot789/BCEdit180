using System.Threading.Tasks;

namespace BCEdit180.Core.Window {
    public interface IDialogManager {
        void ShowMessage(string header, string description);

        void ShowWarning(string header, string description);

        bool ConfirmOkCancel(string header, string description);

        bool ConfirmOkCancel(string header, string description, bool defaultResult);

        ActionProgressViewModel ShowProgressWindow(string header, string description = null);
    }
}