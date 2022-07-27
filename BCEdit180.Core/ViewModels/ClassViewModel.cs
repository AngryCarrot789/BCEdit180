using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.AttributeEditor;
using BCEdit180.Core.Commands;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using JavaAsm.IO;
using REghZy.MVVM.Commands;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    /// <summary>
    /// A view model for a class file (.class). This manages the view models for fields, methods, attributes, etc
    /// </summary>
    public class ClassViewModel : BaseViewModel, ISaveable<ClassNode>, IMessageReceiver<BusyStateMessage> {
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

        private bool isBusy;
        public bool IsBusy {
            get => this.isBusy;
            set {
                RaisePropertyChanged(ref this.isBusy, value);
                this.OpenFileCommand.RaiseCanExecuteChanged();
                this.ReloadFileCommand.RaiseCanExecuteChanged();
                this.SaveFileCommand.RaiseCanExecuteChanged();
                this.SaveFileAsCommand.RaiseCanExecuteChanged();
            }
        }


        public ExtendedRelayCommand OpenFileCommand { get; }

        public ExtendedRelayCommand ReloadFileCommand { get; }

        public ExtendedRelayCommand SaveFileCommand { get; }

        public ExtendedRelayCommand SaveFileAsCommand { get; }

        public ICommand ExitCommand { get; }

        private static ClassNode CreateBlankClass() {
            ClassNode node = new ClassNode() {
                Name = new ClassName("BlankClass"),
                Access = ClassAccessModifiers.Public | ClassAccessModifiers.Super,
                MajorVersion = ClassVersion.Java8,
                SuperName = new ClassName("java/lang/Object")
            };

            node.Methods.Add(new MethodNode() {
                Owner = node,
                Access = MethodAccessModifiers.Public,
                Name = "<init>",
                Descriptor = new MethodDescriptor(new TypeDescriptor(PrimitiveType.Void, 0), new List<TypeDescriptor>()),
                MaxLocals = 1,
                Instructions = new InstructionList() {
                    new VariableInstruction(Opcode.ALOAD) { VariableIndex = 0 },
                    new MethodInstruction(Opcode.INVOKESPECIAL) {
                        Owner = new ClassName("java/lang/Object"),
                        Descriptor = new MethodDescriptor(new TypeDescriptor(PrimitiveType.Void, 0), new List<TypeDescriptor>()),
                        Name = "<init>"
                    },

                    new VariableInstruction(Opcode.ALOAD) { VariableIndex = 0 },
                    new MethodInstruction(Opcode.INVOKEVIRTUAL) {
                        Owner = node.Name,
                        Descriptor = new MethodDescriptor(new TypeDescriptor(PrimitiveType.Void, 0), new List<TypeDescriptor>()),
                        Name = "blankMethod"
                    },

                    new SimpleInstruction(Opcode.RETURN)
                }
            });

            node.Methods.Add(new MethodNode() {
                Owner = node,
                Access = MethodAccessModifiers.Public,
                Name = "blankMethod",
                Descriptor = new MethodDescriptor(new TypeDescriptor(PrimitiveType.Void, 0), new List<TypeDescriptor>()),
                MaxLocals = 1,
                Instructions = new InstructionList() {
                    new SimpleInstruction(Opcode.RETURN)
                }
            });

            node.Fields.Add(new FieldNode() {
                Owner = node,
                Name = "blankField",
                Access = FieldAccessModifiers.Public,
                Descriptor = new TypeDescriptor(PrimitiveType.Integer, 0),
                ConstantValue = 420
            });

            return node;
        }

        public ClassViewModel() : this(CreateBlankClass()) {

        }

        public ClassViewModel(ClassNode node) {
            MessageManager.RegisterHandler<BusyStateMessage>(this);
            this.Node = node;
            this.ExitCommand = new RelayCommand(() => Environment.Exit(0));
            this.OpenFileCommand = new ExtendedRelayCommand(OpenFile, () => !this.IsBusy );
            this.ReloadFileCommand = new ExtendedRelayCommand(ReloadFile, () => !this.IsBusy );
            this.SaveFileCommand = new ExtendedRelayCommand(SaveClassFile, () => !this.IsBusy );
            this.SaveFileAsCommand = new ExtendedRelayCommand(SaveClassFileAs, () => !this.IsBusy );
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
            else if (this.FilePath != null) {
                Dialog.Message.ShowInformationDialog("File no longer exists", "Cannot reload the file, because the file no longer exists: " + this.FilePath);
            }
        }

        public void ReadClassFile(string path, bool showProgressDialog = true) {
            // my attempt at a semi-async class parser
            try {
                this.IsBusy = true;
                if (showProgressDialog) {
                    ActionProgressViewModel vm = Dialog.Message.ShowProgressWindow("Loading class file", "Reading file " + path);
                    Task.Run(async () => {
                        await Task.Delay(100);
                        AppProxy.Proxy.InvokeSync(() => {
                            using (BufferedStream input = new BufferedStream(File.OpenRead(path), 8192)) {
                                this.Node = ClassFile.ParseClass(input);
                            }

                            vm.Description = "Parsing classfile... ";
                            Task.Run(async () => {
                                await Task.Delay(100);
                                AppProxy.Proxy.InvokeSync(() => {
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
            finally {
                this.IsBusy = false;
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

        public void OpenFile() {
            if (Dialog.File.OpenFileDialog("Select a class file to open", "ClassFile|*.class|All|*.*", out string path).Result) {
                if (File.Exists(path)) {
                    ReadClassFileAndShowDialog(path);
                }
                else {
                    Dialog.Message.ShowWarningDialog("No such file", "File does not exist: " + path);
                }
            }
        }

        public void SaveClassFile() {
            try {
                this.IsBusy = true;
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
                        Dialog.Message.ShowWarningDialog("Failed to process class", "Failed to process/save class before writing to file: " + this.FilePath + "\n" + e);
                        return;
                    }

                    try {
                        using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                            ClassFile.WriteClass(output, this.Node);
                        }
                    }
                    catch (Exception e) {
                        Dialog.Message.ShowWarningDialog("Failed to write to file", "Failed to write class to file: " + this.FilePath + "\n" + e);
                    }

                    // ClassFile.WriteClass mutates ClassNode's attributes,
                    // so reloading will fix
                    ReadClassFileAndShowDialog(this.FilePath);
                }
                else {
                    SaveClassFileAs();
                }
            }
            catch (Exception e) {
                Dialog.Message.ShowWarningDialog("Failed to save", "Failed to save file: " + this.FilePath + "\n" + e);
            }
            finally {
                this.IsBusy = false;
            }
        }

        public void SaveClassFileAs() {
            this.IsBusy = true;
            try {
                if (Dialog.File.OpenSaveDialog("Save a class file", "ClassFile|*.class|All|*.*", out string path).Result) {
                    if (!File.Exists(path) || Dialog.Message.ConfirmOkCancel("Overwrite file", "File already exists. Overwrite " + path + "?", true).Result) {
                        this.FilePath = path;

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
                            Dialog.Message.ShowWarningDialog("Failed to process class", "Failed to process/save class before writing to file: " + this.FilePath + "\n" + e);
                            return;
                        }

                        try {
                            using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                                ClassFile.WriteClass(output, this.Node);
                            }
                        }
                        catch (Exception e) {
                            Dialog.Message.ShowWarningDialog("Failed to write to file", "Failed to write class to file: " + this.FilePath + "\n" + e);
                        }

                        // ClassFile.WriteClass mutates ClassNode's attributes,
                        // so reloading will fix
                        ReadClassFileAndShowDialog(this.FilePath);
                    }
                }
            }
            catch (Exception e) {
                Dialog.Message.ShowWarningDialog("Failed to save", "Failed to save file: " + e);
            }
            finally {
                this.IsBusy = false;
            }
        }

        public void HandleMessage(BusyStateMessage message) {
            this.IsBusy = message.IsBusy;
        }
    }
}