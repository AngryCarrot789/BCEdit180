using System.Threading.Tasks;

namespace BCEdit180.Core.Dialogs {
    public interface IDialogManager {
        Task ShowInformationDialog(string header, string description);

        Task ShowWarningDialog(string header, string description);

        Task<bool> ConfirmOkCancel(string header, string description);

        Task<bool> ConfirmOkCancel(string header, string description, bool defaultResult);

        ActionProgressViewModel ShowProgressWindow(string header, string description = null);
    }
}