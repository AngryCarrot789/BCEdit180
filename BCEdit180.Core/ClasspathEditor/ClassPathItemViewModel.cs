using System.IO;
using System.Net;
using System.Windows.Input;
using BCEdit180.Core.Window;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ClasspathEditor {
    public class ClassPathItemViewModel : BaseViewModel {
        private string filePath;
        public string FilePath {
            get => this.filePath;
            set => RaisePropertyChanged(ref this.filePath, value);
        }

        public ICommand SelectFileDialogCommand { get; }
        public ICommand SelectFolderDialogCommand { get; }

        public ClassPathItemViewModel() {
            this.SelectFileDialogCommand = new RelayCommand(SelectFileDialogAction);
            this.SelectFolderDialogCommand = new RelayCommand(SelectFolderDialogAction);
        }

        public void SelectFileDialogAction() {
            if (Dialogs.File.OpenFileDialog("Select a directory or file", "ClassFile|*.class|All|*.*", out string file).Result) {
                this.FilePath = file;
            }
        }

        public void SelectFolderDialogAction() {
            if (Dialogs.File.OpenFolderDialog("Select a directory or file", out string folder).Result) {
                this.FilePath = folder;
            }
        }

        public bool FileExists() {
            return !string.IsNullOrEmpty(this.FilePath) && File.Exists(this.FilePath);
        }

        public bool DirectoryExists() {
            return !string.IsNullOrEmpty(this.FilePath) && Directory.Exists(this.FilePath);
        }
    }
}