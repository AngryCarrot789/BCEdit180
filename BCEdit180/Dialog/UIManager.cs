using System.Threading.Tasks;
using System.Windows;
using BCEdit180.Core;

namespace BCEdit180.Dialog {
    public class DialogManager : IUIManager {
        public Task ShowMessage(string title, string message) {
            MessageBox.Show(message, title);
            return Task.CompletedTask;
        }
    }
}