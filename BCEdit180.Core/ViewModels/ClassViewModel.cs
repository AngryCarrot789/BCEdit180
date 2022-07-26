using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BCEdit180.Core.AttributeEditor;
using BCEdit180.Core.Commands;
using BCEdit180.Core.ErrorReporting;
using BCEdit180.Core.Messaging;
using BCEdit180.Core.Messaging.Messages;
using BCEdit180.Core.Messaging.Messages.ErrorReporting;
using BCEdit180.Core.Utils;
using BCEdit180.Core.Window;
using JavaAsm;
using JavaAsm.Instructions;
using JavaAsm.Instructions.Types;
using JavaAsm.IO;
using REghZy.MVVM.ViewModels;

namespace BCEdit180.Core.ViewModels {
    /// <summary>
    /// A view model for a class file (.class). This manages the view models for fields, methods, attributes, etc
    /// </summary>
    public class ClassViewModel : BaseViewModel, IDisposable, ISaveable<ClassNode>, IMessageReceiver<BusyStateMessage> {
        public ClassListViewModel ClassList { get; set; }

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
        
        public ExtendedRelayCommand ReloadFileCommand { get; }

        public ExtendedRelayCommand SaveFileCommand { get; }

        public ExtendedRelayCommand SaveFileAsCommand { get; }

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
                this.ReloadFileCommand.RaiseCanExecuteChanged();
                this.SaveFileCommand.RaiseCanExecuteChanged();
                this.SaveFileAsCommand.RaiseCanExecuteChanged();
            }
        }

        private bool wasOpenedByOuterClass;
        public bool WasOpenedByOuterClass {
            get => this.wasOpenedByOuterClass;
            set => RaisePropertyChanged(ref this.wasOpenedByOuterClass, value);
        }

        public ICommand RemoveSelfCommand { get; }

        public ClassViewModel() : this(CreateBlankClass()) {

        }

        public ClassViewModel(ClassNode node) {
            MessageDispatcher.RegisterHandler<BusyStateMessage>(this);
            this.Node = node;
            this.ReloadFileCommand = new ExtendedRelayCommand(ReloadFile, () => !this.IsBusy );
            this.SaveFileCommand = new ExtendedRelayCommand(SaveClassFile, () => !this.IsBusy );
            this.SaveFileAsCommand = new ExtendedRelayCommand(SaveClassFileAs, () => !this.IsBusy );
            this.ClassInfo = new ClassInfoViewModel(this);
            this.MethodList = new MethodListViewModel(this);
            this.FieldList = new FieldListViewModel(this);
            this.SourceCode = new SourceCodeViewModel(this);
            this.ClassAttributes = new ClassAttributeEditorViewModel(this);
            this.RemoveSelfCommand = new ExtendedRelayCommand(() => this.ClassList?.RemoveClass(this), () => this.ClassList != null);
            Load(node);
        }

        public void Dispose() {
            MessageDispatcher.UnregisterHandler<BusyStateMessage>(this);
            // this.ClassAttributes.Dispose();
            // this.ClassInfo.Dispose();
            this.MethodList.Dispose();
            this.FieldList.Dispose();
        }

        /// <summary>
        /// If the file exists, it is read from the file system
        /// </summary>
        public void ReloadFile() {
            if (this.FilePath != null && File.Exists(this.FilePath)) {
                ReadClassFileAndShowDialog(this.FilePath);
            }
            else if (this.FilePath != null) {
                Dialogs.Message.ShowMessage("File no longer exists", "Cannot reload the file, because the file no longer exists: " + this.FilePath);
            }
        }

        public void ReadClassFile(string path, bool showProgressDialog = true, ActionProgressViewModel actionProgress = null, bool closeDialog = true) {
            // my attempt at a semi-async class parser

            #if DEBUG

            this.IsBusy = true;
            MessageDispatcher.Publish(new AddMessage() { Message = "Reading file..."});
            using (BufferedStream input = new BufferedStream(File.OpenRead(path))) {
                this.Node = ClassFile.ParseClass(input);
                // Dialogs.Message.ShowMessage("Loaded", "Loaded file: " + this.FilePath);
            }

            this.FilePath = path;
            Load(this.Node);
            MessageDispatcher.Publish(new AddMessage() { Message = "Successfully read file"});
            this.IsBusy = false;

            #else

            this.IsBusy = true;
            if (showProgressDialog) {
                ActionProgressViewModel vm = actionProgress ?? Dialogs.Message.ShowProgressWindow("Loading class file", "Reading file " + path);
                Task.Run(async () => {
                    await Task.Delay(100);
                    await AppProxy.Proxy.DispatchInvokeAsync(() => {
                        if (File.Exists(path)) {
                            try {
                                using (BufferedStream input = new BufferedStream(File.OpenRead(path), 8192)) {
                                    this.Node = ClassFile.ParseClass(input);
                                }
                            }
                            catch (Exception e) {
                                vm.Description = "Fail: " + e.Message;
                                Dialogs.Message.ShowWarning("Error reading classfile", "Error while parsing class from file: " + e.Message + "\n" + e);
                                if (closeDialog) {
                                    vm.CloseDialog();
                                }

                                this.IsBusy = false;
                                return;
                            }

                            vm.Description = "Parsing classfile... ";
                            Task.Run(async () => {
                                await Task.Delay(100);
                                await AppProxy.Proxy.DispatchInvokeAsync(() => {
                                    this.FilePath = path;
                                    try {
                                        Load(this.Node);
                                    }
                                    catch (Exception e) {
                                        vm.Description = "Fail: " + e.Message;
                                        Dialogs.Message.ShowWarning("Error loading class", "Error while loading class from ClassNode: " + e.Message + "\n" + e);
                                        if (closeDialog) {
                                            vm.CloseDialog();
                                        }

                                        this.IsBusy = false;
                                        return;
                                    }

                                    if (closeDialog) {
                                        vm.CloseDialog();
                                    }

                                    this.IsBusy = false;
                                });
                            });
                        }
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

            #endif
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
            node.Attributes.Clear();
            this.ClassInfo.Save(node);
            this.ClassAttributes.Save(node);
            this.MethodList.Save(node);
            this.FieldList.Save(node);
        }

        public void SaveClassFile() {
            // DEBUG MODE will allow exceptions to be thrown, so the debugger can catch them
            // RELEASE MODE shows a dialog with the errors

            #if DEBUG

            try {
                MessageDispatcher.Publish(new AddMessage() { Message = "Saving file..." });
                this.IsBusy = true;
                if (this.FilePath != null) {
                    if (File.Exists(this.FilePath)) {
                        string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                        if (File.Exists(backupPath)) {
                            File.Delete(backupPath);
                        }

                        File.Copy(this.FilePath, backupPath);
                    }

                    SaveClassToFile();

                    // Fixed :)
                    // ClassFile.WriteClass mutates ClassNode's attributes,
                    // so reloading will fix
                    // ReadClassFileAndShowDialog(this.FilePath);
                    MessageDispatcher.Publish(new AddMessage() { Message = "Saved successfully :)" });
                }
                else {
                    SaveClassFileAs();
                }
            }
            finally {
                this.IsBusy = false;
            }

            #else

            try {
                MessageDispatcher.Publish(new AddMessage("Saving file..."));
                this.IsBusy = true;
                if (this.FilePath != null) {
                    if (File.Exists(this.FilePath)) {
                        string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                        if (File.Exists(backupPath)) {
                            File.Delete(backupPath);
                        }

                        File.Copy(this.FilePath, backupPath);
                    }

                    SaveClassToFile();
                    MessageDispatcher.Publish(new AddMessage("Successfully saved file"));
                    // Fixed :)
                    // ClassFile.WriteClass mutates ClassNode's attributes,
                    // so reloading will fix
                    // ReadClassFileAndShowDialog(this.FilePath);
                }
                else {
                    SaveClassFileAs();
                }
            }
            catch (Exception e) {
                Dialogs.Message.ShowWarning("Failed to save", "Failed to save file: " + this.FilePath + "\n" + e);
            }
            finally {
                this.IsBusy = false;
            }

            #endif
        }

        public void SaveClassFileAs() {
            this.IsBusy = true;
            try {
                MessageDispatcher.Publish(new AddMessage("Selecting file from dialog..."));
                if (Dialogs.File.OpenSaveDialog("Save a class file", "ClassFile|*.class|All|*.*", out string path).Result) {
                    if (!File.Exists(path) || Dialogs.Message.ConfirmOkCancel("Overwrite file", "File already exists. Overwrite " + path + "?", true)) {
                        this.FilePath = path;

                        MessageDispatcher.Publish(new AddMessage("Saving file..."));
                        if (File.Exists(this.FilePath)) {
                            string backupPath = Path.Combine(Path.GetDirectoryName(this.FilePath) ?? "", "backup_" + Path.GetFileName(this.FilePath));
                            if (File.Exists(backupPath)) {
                                File.Delete(backupPath);
                            }

                            File.Copy(this.FilePath, backupPath);
                        }

                        SaveClassToFile();
                        MessageDispatcher.Publish(new AddMessage("Successfully saved file"));
                        // Fixed :)
                        // ClassFile.WriteClass mutates ClassNode's attributes,
                        // so reloading will fix
                        // ReadClassFileAndShowDialog(this.FilePath);
                    }
                }
            }
            catch (Exception e) {
                MessageDispatcher.Publish(new AddMessage("Failed to save file"));
                Dialogs.Message.ShowWarning("Failed to save", "Failed to save file: " + e);
            }
            finally {
                this.IsBusy = false;
            }
        }

        public void SaveClassToFile() {
            SaveClassToFile(this.FilePath);
        }

        public void SaveClassToFile(string classFile) {
            // Makes debugging easier; using #if DEBUG inside a catch block to rethrow is literally almost useless

            #if DEBUG

            this.FilePath = classFile;
            Save(this.Node);
            using (BufferedStream output = new BufferedStream(File.OpenWrite(classFile))) {
                ClassFile.WriteClass(output, this.Node);
            }

            #else

            try {
                Save(this.Node);
            }
            catch (Exception e) {
                Dialogs.Message.ShowWarning("Failed to process class", "Failed to process/save class before writing to file: " + this.FilePath + "\n" + e);
                return;
            }

            try {
                using (BufferedStream output = new BufferedStream(File.OpenWrite(this.FilePath))) {
                    ClassFile.WriteClass(output, this.Node);
                }
            }
            catch (Exception e) {
                Dialogs.Message.ShowWarning("Failed to write to file", "Failed to write class to file: " + this.FilePath + "\n" + e);
            }

            #endif
        }

        public void HandleMessage(BusyStateMessage message) {
            this.IsBusy = message.IsBusy;
        }

        private static ClassNode CreateBlankClass() {
            ClassNode node = new ClassNode() {
                Name = new ClassName("BlankClass"),
                Access = ClassAccessModifiers.Public | ClassAccessModifiers.Super,
                MajorVersion = ClassVersion.Java8,
                SuperName = new ClassName("java/lang/Object")
            };

            node.Interfaces.Add(new ClassName("my/interface/called/MyInterface"));
            node.Interfaces.Add(new ClassName("java/lang/Cloneable"));

            Label label = new Label() { Index = 1 };

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

                    new LdcInstruction(Opcode.LDC) {
                        Value = (int) 1
                    },
                    new JumpInstruction(Opcode.IF_ICMPEQ) {
                        Target = label
                    },

                    label,

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
    }
}