using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using JavaAsm;
using JavaAsm.IO;
using Microsoft.Win32;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.ViewModels {
    public class ClassViewModel : BaseViewModel {
        public ClassNode Node { get; private set; }

        public ClassInfoViewModel ClassInfo { get; }
        public MethodListViewModel MethodList { get; }
        public FieldListViewModel FieldList { get; }

        public SourceCodeViewModel SourceCode { get; }

        private string filePath;
        public string FilePath {
            get => this.filePath;
            set => RaisePropertyChanged(ref this.filePath, value);
        }

        public ICommand OpenFileCommand { get; }

        public ICommand SaveFileCommand { get; }

        public ICommand SaveFileAsCommand { get; }

        public ICommand ExitCommand { get; }

        public ClassViewModel(ClassNode node) {
            this.Node = node;
            this.ExitCommand = ApplicationCommands.Close;
            this.OpenFileCommand = new RelayCommand(OpenFile);
            this.SaveFileCommand = new RelayCommand(Save);
            this.SaveFileAsCommand = new RelayCommand(SaveAs);

            this.ClassInfo = new ClassInfoViewModel(this);
            this.MethodList = new MethodListViewModel(this);
            this.FieldList = new FieldListViewModel(this);
            this.SourceCode = new SourceCodeViewModel(this);

            LoadClass(this.Node);
        }

        public void ReadClassFile(string path) {
            using (BufferedStream input = new BufferedStream(File.OpenRead(path))) {
                this.Node = ClassFile.ParseClass(input);
            }

            this.FilePath = path;
            LoadClass(this.Node);
        }

        public void LoadClass(ClassNode node) {
            this.ClassInfo.Load(node);
            this.MethodList.Load(node);
            this.FieldList.Load(node);
        }

        public void SaveClass(ClassNode node) {
            this.ClassInfo.Save(node);
            this.MethodList.Save(node);
            this.FieldList.Save(node);
        }

        public void OpenFile() {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "ClassFile|*.class|All|*.*";
            dialog.Title = "Select a class file to open";
            if (dialog.ShowDialog() == true) {
                string path = dialog.FileName;
                if (File.Exists(path)) {
                    ReadClassFile(path);
                }
                else {
                    MessageBox.Show("File does not exist: " + path, "No such file");
                }
            }
        }

        public void Save() {
            try {
                if (File.Exists(this.FilePath)) {
                    string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                    if (File.Exists(backupPath)) {
                        File.Delete(backupPath);
                    }

                    File.Copy(this.FilePath, backupPath);
                }

                SaveClass(this.Node);
                using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                    ClassFile.WriteClass(output, this.Node);
                }

                // ClassFile.WriteClass mutates ClassNode's attributes,
                // so reloading will fix
                ReadClassFile(this.FilePath);
            }
            catch (Exception e) {
                MessageBox.Show("Failed to save file: " + e, "Failed to save");
            }
        }

        public void SaveAs() {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "ClassFile|*.class|All|*.*";
            dialog.Title = "Save a class file";
            if (dialog.ShowDialog() == true) {
                string path = dialog.FileName;
                if (!File.Exists(path) || MessageBox.Show("File already exists. Overwrite " + path + "?", "Overwrite file", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK) {
                    this.FilePath = path;

                    try {
                        if (File.Exists(this.FilePath)) {
                            string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                            if (File.Exists(backupPath)) {
                                File.Delete(backupPath);
                            }

                            File.Copy(this.FilePath, backupPath);
                        }

                        try {
                            SaveClass(this.Node);
                        }
                        catch (Exception e) {
                            MessageBox.Show("Failed to process/save class before writing to file: " + e, "Failed to process class");
                            return;
                        }

                        using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                            ClassFile.WriteClass(output, this.Node);
                        }

                        // ClassFile.WriteClass mutates ClassNode's attributes,
                        // so reloading will fix
                        ReadClassFile(this.FilePath);
                    }
                    catch (Exception e) {
                        MessageBox.Show("Failed to save file: " + e, "Failed to save");
                    }
                }
            }
        }
    }
}