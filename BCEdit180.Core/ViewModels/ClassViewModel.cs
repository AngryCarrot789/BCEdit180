using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.AttributeEditor;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.IO;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    /// <summary>
    /// A view model for a class file (.class). This manages the view models for fields, methods, attributes, etc
    /// </summary>
    public class ClassViewModel : BaseViewModel, ISaveable<ClassNode> {
        /// <summary>
        /// The class node handle that this class will load and save into
        /// <para>
        /// This should never really be referenced anywhere else, apart from when creating fields and classes
        /// </para>
        /// </summary>
        public ClassNode Node { get; private set; }

        /// <summary>
        /// General class information
        /// </summary>
        public ClassInfoViewModel ClassInfo { get; }

        /// <summary>
        /// This class' methods
        /// </summary>
        public MethodListViewModel MethodList { get; }

        /// <summary>
        /// This class' fields
        /// </summary>
        public FieldListViewModel FieldList { get; }

        /// <summary>
        /// A source-code generator (very WIP)
        /// </summary>
        public SourceCodeViewModel SourceCode { get; }

        /// <summary>
        /// Class attributes
        /// </summary>
        public ClassAttributeEditorViewModel ClassAttributes { get; }

        private string filePath;

        public string FilePath {
            get => this.filePath;
            set => RaisePropertyChanged(ref this.filePath, value);
        }

        public ICommand OpenFileCommand { get; }

        public ICommand ReloadFileCommand { get; }

        public ICommand SaveFileCommand { get; }

        public ICommand SaveFileAsCommand { get; }

        public ICommand ExitCommand { get; }

        private static ClassNode CreateBlankClass() {
            return new ClassNode() {
                Name = new ClassName("BlankClass"),
                Access = ClassAccessModifiers.Public | ClassAccessModifiers.Super,
                MajorVersion = ClassVersion.Java8,
                SuperName = new ClassName("java/lang/Object")
            };
        }

        public ClassViewModel() : this(CreateBlankClass()) {

        }

        public ClassViewModel(ClassNode node) {
            this.Node = node;
            this.ExitCommand = new RelayCommand(()=> Environment.Exit(0));
            this.OpenFileCommand = new RelayCommand(OpenFile);
            this.ReloadFileCommand = new RelayCommand(ReloadFile);
            this.SaveFileCommand = new RelayCommand(Save);
            this.SaveFileAsCommand = new RelayCommand(SaveAs);

            this.ClassInfo = new ClassInfoViewModel(this);
            this.MethodList = new MethodListViewModel(this);
            this.FieldList = new FieldListViewModel(this);
            this.SourceCode = new SourceCodeViewModel(this);
            this.ClassAttributes = new ClassAttributeEditorViewModel(this);
            Load(node);
        }

        /// <summary>
        /// If the file exists, it is read from the file system
        /// </summary>
        public void ReloadFile() {
            if (this.FilePath != null && File.Exists(this.FilePath)) {
                ReadClassFileAndShowDialog(this.FilePath);
            }
            else {

            }
        }

        public void ReadClassFile(string path, bool showProgressDialog = true) {
            // my attempt at a semi-async class parser
            if (showProgressDialog) {
                ActionProgressViewModel vm = Dialog.Message.ShowProgressWindow("Loading class file", "Reading file " + path);
                Task.Run(async () => {
                    await Task.Delay(100);
                    AppServices.Services.RunSync(() => {
                        using (BufferedStream input = new BufferedStream(File.OpenRead(path), 8192)) {
                            this.Node = ClassFile.ParseClass(input);
                        }

                        vm.Description = "Parsing classfile... ";
                        Task.Run(async () => {
                            await Task.Delay(100);
                            AppServices.Services.RunSync(() => {
                                this.FilePath = path;
                                Load(this.Node);
                                vm.CloseDialog();
                            });
                        });
                    });
                });
            }
            else {
                using (BufferedStream input = new BufferedStream(File.OpenRead(path))) {
                    this.Node = ClassFile.ParseClass(input);
                }

                this.FilePath = path;
                Load(this.Node);
            }
        }

        public void ReadClassFileAndShowDialog(string path) {
            ReadClassFile(path, true);
        }

        public void Load(ClassNode node) {
            this.ClassInfo.Load(node);
            this.ClassAttributes.Load(node);
            this.MethodList.Load(node);
            this.FieldList.Load(node);
        }

        public void Save(ClassNode node) {
            this.ClassInfo.Save(node);
            this.ClassAttributes.Save(node);
            this.MethodList.Save(node);
            this.FieldList.Save(node);
        }

        public async void OpenFile() {
            if (await Dialog.File.OpenFileDialog("Select a class file to open", "ClassFile|*.class|All|*.*", out string path)) {
                if (File.Exists(path)) {
                    ReadClassFileAndShowDialog(path);
                }
                else {
                    await Dialog.Message.ShowWarningDialog("No such file", "File does not exist: " + path);
                }
            }
        }

        public async void Save() {
            try {
                if (this.FilePath != null) {
                    if (File.Exists(this.FilePath)) {
                        string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                        if (File.Exists(backupPath)) {
                            File.Delete(backupPath);
                        }

                        File.Copy(this.FilePath, backupPath);
                    }

                    try {
                        Save(this.Node);
                    }
                    catch (Exception e) {
                        await Dialog.Message.ShowWarningDialog("Failed to process class", "Failed to process/save class before writing to file: " + this.FilePath + "\n" + e);
                        return;
                    }

                    try {
                        using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                            ClassFile.WriteClass(output, this.Node);
                        }
                    }
                    catch (Exception e) {
                        await Dialog.Message.ShowWarningDialog("Failed to write to file", "Failed to write class to file: " + this.FilePath + "\n" + e);
                    }

                    // ClassFile.WriteClass mutates ClassNode's attributes,
                    // so reloading will fix
                    ReadClassFileAndShowDialog(this.FilePath);
                }
                else {
                    SaveAs();
                }
            }
            catch (Exception e) {
                await Dialog.Message.ShowWarningDialog("Failed to save", "Failed to save file: " + this.FilePath + "\n" + e);
            }
        }

        public async void SaveAs() {
            if (await Dialog.File.OpenSaveDialog("Save a class file", "ClassFile|*.class|All|*.*", out string path)) {
                if (!File.Exists(path) || await Dialog.Message.ConfirmOkCancel("Overwrite file", "File already exists. Overwrite " + path + "?", true)) {
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
                            Save(this.Node);
                        }
                        catch (Exception e) {
                            await Dialog.Message.ShowWarningDialog("Failed to process class", "Failed to process/save class before writing to file: " + this.FilePath + "\n" + e);
                            return;
                        }

                        try {
                            using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                                ClassFile.WriteClass(output, this.Node);
                            }
                        }
                        catch (Exception e) {
                            await Dialog.Message.ShowWarningDialog("Failed to write to file", "Failed to write class to file: " + this.FilePath + "\n" + e);
                        }

                        // ClassFile.WriteClass mutates ClassNode's attributes,
                        // so reloading will fix
                        ReadClassFileAndShowDialog(this.FilePath);
                    }
                    catch (Exception e) {
                        await Dialog.Message.ShowWarningDialog("Failed to save", "Failed to save file: " + e);
                    }
                }
            }
        }
    }
}