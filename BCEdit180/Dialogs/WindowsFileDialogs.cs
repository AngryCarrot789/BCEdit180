using System.IO;
using System.Threading.Tasks;
using BCEdit180.Core.Window;
using Microsoft.Win32;

namespace BCEdit180.Dialogs {
    public class WindowsFileDialogs : IFileDialog {
        public Task<bool> OpenFileDialog(string title, string filter, out string path) {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = filter,
                Title = title
            };

            if (dialog.ShowDialog() == true && File.Exists(dialog.FileName)) {
                path = dialog.FileName;
                return Task.FromResult(true);
            }
            else {
                path = null;
                return Task.FromResult(false);
            }
        }

        public Task<bool> OpenSaveDialog(string title, string filter, out string path) {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = filter,
                Title = title
            };

            if (dialog.ShowDialog() == true) {
                path = dialog.FileName;
                return Task.FromResult(true);
            }
            else {
                path = null;
                return Task.FromResult(false);
            }
        }

        public Task<bool> OpenFolderDialog(string title, out string path) {
            FolderPickerDialog picker = new FolderPickerDialog() {
                Title = title
            };

            if (picker.ShowDialog() == true && Directory.Exists(picker.ResultPath)) {
                path = picker.ResultPath;
                return Task.FromResult(true);
            }
            else {
                path = null;
                return Task.FromResult(false);
            }
        }
    }
}
