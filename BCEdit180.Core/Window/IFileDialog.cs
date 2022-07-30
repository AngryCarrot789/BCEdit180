using System.Threading.Tasks;

namespace BCEdit180.Core.Window {
    public interface IFileDialog {
        Task<bool> OpenFileDialog(string title, string filter, out string path);
        Task<bool> OpenSaveDialog(string title, string filter, out string path);
        Task<bool> OpenFolderDialog(string title, out string path);
    }
}