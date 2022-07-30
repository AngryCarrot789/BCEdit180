using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using BCEdit180.AppSettings;
using BCEdit180.Core;
using BCEdit180.Core.AttributeEditor;
using BCEdit180.Core.AttributeEditor.Classes;
using BCEdit180.Core.CodeEditing;
using BCEdit180.Core.CodeEditing.Bytecode.Instructions;
using BCEdit180.Core.Commands;
using BCEdit180.Core.Modals;
using BCEdit180.Core.Utils;
using BCEdit180.Core.ViewModels;
using BCEdit180.Core.Window;
using BCEdit180.Dialogs;
using BCEdit180.Themes;
using BCEdit180.Windows;
using JavaAsm.Instructions;

namespace BCEdit180 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase {
        private static readonly XmlSerializer XMLSerialiser = new XmlSerializer(typeof(AppSettingsXML));
        private static readonly string ConfigFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BCEditor");
        private static readonly string ConfigFile = Path.Combine(ConfigFolder, "config.xml");

        static MainWindow() {
        }

        public MainWindow() {
            InitializeComponent();
            this.LoadXML();

            // string path = "F:\\IJProjects\\CarrotTools\\out\\production\\CarrotTools\\reghzy\\carrottools\\playerdata\\results\\custom\\tileentity\\TileEntityTimingResult.class";
            // string path = "F:\\MinecraftUtils\\server\\tekkitmain-1.6.4\\CarrotClassSplicer\\World.class";
            // string path = @"F:\IJProjects\CarrotTools\out\production\CarrotTools\reghzy\carrottools\playerdata\results\custom\BlockLookupResult.class";
            // using (BufferedStream stream = new BufferedStream(File.OpenRead(path))) {
            //     ClassNode node = ClassFile.ParseClass(stream);
            //     this.DataContext = new ClassViewModel(node) {
            //         FilePath = path
            //     };
            // }

            // was planning on using a java server to process classfiles, because the asm-all library is pretty good
            // but then i found java-asm, which seems like the exact same as asm-all, except in c# :))
            // (you can download my fork of it, which i downgraded to .NET standard 2.0 + with extra added features, like local variable names.
            // The original version is .NET Standard 2.1, and also lacks features that this program uses)

            // Socket server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            // server.Bind(new IPEndPoint(IPAddress.Any, 12944));
            // server.Listen(1);
            // 
            // Socket client = server.Accept();
            // 
            // DataOutputStream output = new DataOutputStream(new NetworkStream(client));
            // output.WriteInt(69420);
            // output.WriteStringLabelledUTF8("hello there!");
            // 
            // client.Disconnect(false);
            // server.Close();

            // Packet.Setup();
            // Socket server = SocketHelper.CreateServerSocket(IPAddress.Any, 12944);
            // server.Listen(1);
            // PacketSystem system = new PacketSystem(SocketHelper.AcceptClientConnection(server));
            // system.SendPacket(new Packet1Message() { message = "ello there lolol" });
            // system.ProcessSendQueue();
            // system.Connection.Disconnect();
            // server.Close();

        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            this.SaveXML();
        }

        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
        }

        private bool setup;
        public void SetupServices() {
            if (this.setup) {
                throw new InvalidOperationException("Services already setup");
            }

            this.setup = true;
            ServiceManager.SetService<IDialogManager>(new WindowsDialogs());
            ServiceManager.SetService<ITypeEditors>(new WindowsTypeEditors());
            ServiceManager.SetService<IAccessEditor>(new WindowsAccessEditor());
            ServiceManager.SetService<IFileDialog>(new WindowsFileDialogs());
            ServiceManager.SetService<IApplicationProxy>(new WpfApplicationProxy());
            ServiceManager.SetService<IModalManager>(new WindowsModalManager());
            ServiceManager.SetService<ICommandManager>(new WPFCommandManager());
            BytecodeEditorViewModel.BytecodeList = new BytecodeListImpl(this);
            MethodListViewModel.MethodList = new MethodListImpl(this);
            FieldListViewModel.FieldList = new FieldListImpl(this);
        }

        private class BytecodeListImpl : IListSelector<BaseInstructionViewModel> {
            private readonly MainWindow window;

            public BytecodeListImpl(MainWindow window) {
                this.window = window;
            }

            public IEnumerable<BaseInstructionViewModel> SelectedItems {
                get {
                    List<BaseInstructionViewModel> instructions = new List<BaseInstructionViewModel>();
                    foreach (object item in this.window.BytecodeEditorListBox.SelectedItems) {
                        instructions.Add((BaseInstructionViewModel) item);
                    }

                    return instructions;
                }
            }

            public void BringIntoView(BaseInstructionViewModel value) {
                this.window.BytecodeEditorListBox.ScrollIntoView(value);
            }

            public void ScrollToSelectedItem() {
                this.window.BytecodeEditorListBox.ScrollIntoView(this.window.BytecodeEditorListBox.SelectedItem);
            }
        }

        private class MethodListImpl : IListSelector<MethodInfoViewModel> {
            private readonly MainWindow window;

            public MethodListImpl(MainWindow window) {
                this.window = window;
            }

            public IEnumerable<MethodInfoViewModel> SelectedItems {
                get {
                    List<MethodInfoViewModel> instructions = new List<MethodInfoViewModel>();
                    foreach (object item in this.window.MethodList.SelectedItems) {
                        instructions.Add((MethodInfoViewModel) item);
                    }

                    return instructions;
                }
            }

            public void BringIntoView(MethodInfoViewModel value) {
                this.window.MethodList.ScrollIntoView(value);
            }

            public void ScrollToSelectedItem() {
                this.window.MethodList.ScrollIntoView(this.window.MethodList.SelectedItem);
            }
        }

        private class FieldListImpl : IListSelector<FieldInfoViewModel> {
            private readonly MainWindow window;

            public FieldListImpl(MainWindow window) {
                this.window = window;
            }

            public IEnumerable<FieldInfoViewModel> SelectedItems {
                get {
                    List<FieldInfoViewModel> instructions = new List<FieldInfoViewModel>();
                    foreach (object item in this.window.FieldList.SelectedItems) {
                        instructions.Add((FieldInfoViewModel) item);
                    }

                    return instructions;
                }
            }

            public void BringIntoView(FieldInfoViewModel value) {
                this.window.FieldList.ScrollIntoView(value);
            }

            public void ScrollToSelectedItem() {
                this.window.FieldList.ScrollIntoView(this.window.FieldList.SelectedItem);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            ClassViewModel vm = ((ClassListViewModel) this.DataContext).SelectedClass;
            if (vm != null && File.Exists(vm.FilePath)) {
                string folder = Path.GetDirectoryName(vm.FilePath);
                if (folder == null) {
                    return;
                }
                else {
                    Process.Start("explorer.exe", @"/select, """ + vm.FilePath + "\"");
                }
            }
        }

        private void Window_PreviewDrop(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if (files == null || files.Length == 0) {
                    return;
                }

                ((ClassListViewModel) this.DataContext).OpenAndReadClassFiles(files);
                e.Handled = true;
            }
        }

        // i have literally no idea how to do this using only MVVM

        private void RemoveSelectedMethodsClick(object sender, RoutedEventArgs e) {
            if (this.MethodList.SelectedItems.Count == 0) {
                return;
            }

            List<MethodInfoViewModel> remove = new List<MethodInfoViewModel>();
            foreach (object element in this.MethodList.SelectedItems) {
                if (element != null) {
                    remove.Add((MethodInfoViewModel) element);
                }
            }

            if (remove.Count == 0) {
                return;
            }

            ObservableCollection<MethodInfoViewModel> actualMethods = remove[0].MethodList.Methods;
            ObservableCollection<MethodInfoViewModel> removedMethods = remove[0].MethodList.RemovedMethods;
            foreach (MethodInfoViewModel method in remove) {
                actualMethods.Remove(method);
                removedMethods.Add(method);
            }
        }

        private void UndoRemoveMethod(object sender, RoutedEventArgs e) {
            if (this.RemovedMethodList.SelectedItems.Count == 0) {
                return;
            }

            List<MethodInfoViewModel> removed = new List<MethodInfoViewModel>();
            foreach (object element in this.RemovedMethodList.SelectedItems) {
                if (element != null && element is MethodInfoViewModel) {
                    removed.Add((MethodInfoViewModel) element);
                }
            }

            if (removed.Count == 0) {
                return;
            }

            ObservableCollection<MethodInfoViewModel> removedMethods = removed[0].MethodList.RemovedMethods;
            ObservableCollection<MethodInfoViewModel> actualMethods = removed[0].MethodList.Methods;
            foreach (MethodInfoViewModel method in removed) {
                removedMethods.Remove(method);
                actualMethods.Add(method);
            }
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {

        }

        private void RemovedMethodList_GotFocus(object sender, RoutedEventArgs e) {
            this.MethodList.SelectedItem = null;
        }

        private void MethodList_GotFocus(object sender, RoutedEventArgs e) {
            this.RemovedMethodList.SelectedItem = null;
        }

        // field stuff 
        private void RemovedFieldList_GotFocus(object sender, RoutedEventArgs e) {
            this.FieldList.SelectedItem = null;
        }

        private void FieldList_GotFocus(object sender, RoutedEventArgs e) {
            this.RemovedFieldList.SelectedItem = null;
        }

        private void RemoveSelectedFieldsClick(object sender, RoutedEventArgs e) {
            if (this.FieldList.SelectedItems.Count == 0) {
                return;
            }

            List<FieldInfoViewModel> remove = new List<FieldInfoViewModel>();
            foreach (object element in this.FieldList.SelectedItems) {
                if (element != null) {
                    remove.Add((FieldInfoViewModel) element);
                }
            }

            if (remove.Count == 0) {
                return;
            }

            ObservableCollection<FieldInfoViewModel> actualMethods = remove[0].FieldList.Fields;
            ObservableCollection<FieldInfoViewModel> removedMethods = remove[0].FieldList.RemovedFields;
            foreach (FieldInfoViewModel removed in remove) {
                actualMethods.Remove(removed);
                removedMethods.Add(removed);
            }
        }

        private void UndoRemoveField(object sender, RoutedEventArgs e) {
            if (this.RemovedMethodList.SelectedItems.Count == 0) {
                return;
            }

            List<FieldInfoViewModel> removed = new List<FieldInfoViewModel>();
            foreach (object element in this.RemovedMethodList.SelectedItems) {
                if (element != null && element is FieldInfoViewModel) {
                    removed.Add((FieldInfoViewModel) element);
                }
            }

            if (removed.Count == 0) {
                return;
            }

            ObservableCollection<FieldInfoViewModel> removedMethods = removed[0].FieldList.RemovedFields;
            ObservableCollection<FieldInfoViewModel> actualMethods = removed[0].FieldList.Fields;
            foreach (FieldInfoViewModel method in removed) {
                removedMethods.Remove(method);
                actualMethods.Add(method);
            }
        }

        private void RemoveSelectedInnerClasses(object sender, RoutedEventArgs e) {
            if (this.InnerClassesList.SelectedItems.Count == 0) {
                return;
            }

            List<InnerClassViewModel> remove = new List<InnerClassViewModel>();
            foreach (object element in this.InnerClassesList.SelectedItems) {
                if (element != null && element is InnerClassViewModel) {
                    remove.Add((InnerClassViewModel) element);
                }
            }

            if (remove.Count == 0) {
                return;
            }

            ClassAttributeEditorViewModel editor = remove[0].Class;
            foreach (InnerClassViewModel inner in remove) {
                editor.InnerClasses.Remove(inner);
            }
        }

        private bool isDragging;

        private void BytecodeEditorListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            // ListBox list = (ListBox) sender;
            // IEnumerable<BaseInstructionViewModel> selected = BytecodeEditorViewModel.SelectedInstructionProvider.SelectedItems;
            // List<Instruction> instructions = new List<Instruction>();
            // foreach (BaseInstructionViewModel instruction in selected) {
            //     instruction.Save(instruction.Instruction);
            //     instructions.Add(instruction.Instruction);
            // }
            // 
            // this.isDragging = true;
            // DragDropEffects effect = DragDrop.DoDragDrop(list, instructions, Keyboard.IsKeyDown(Key.LeftCtrl) ? DragDropEffects.Copy : DragDropEffects.Move);
            // this.isDragging = false;
            // if (effect == DragDropEffects.Move) {
            //     BytecodeEditorViewModel vm = (BytecodeEditorViewModel) list.DataContext;
            //     foreach(BaseInstructionViewModel instruction in selected) {
            //         vm.Instructions.Remove(instruction);
            //     }
            // }
        }

        private void BytecodeEditorListBox_Drop(object sender, DragEventArgs e) {
            // if (this.isDragging) {
            //     e.Handled = true;
            //     return;
            // }
            // 
            // List<Instruction> instructions = e.Data.GetData(typeof(List<Instruction>)) as List<Instruction>;
            // if (instructions != null) {
            // 
            // }
        }

        private void OnWindowKeyDown(object sender, KeyEventArgs e) {
            if (Keyboard.IsKeyDown(Key.F) && (Keyboard.Modifiers & ModifierKeys.Control) != 0) {
                this.FindBox.Focus();
                this.FindBox.SelectAll();
            }
            // else if (Keyboard.IsKeyDown(Key.S) && (Keyboard.Modifiers & ModifierKeys.Control) != 0) {
            //     ((ClassListViewModel) this.DataContext).SelectedClass?.SaveClassFile();
            // }
        }

        public void LoadXML() {
            try {
                if (File.Exists(ConfigFile)) {
                    using (BufferedStream stream = new BufferedStream(File.OpenRead(ConfigFile))) {
                        AppSettingsXML settings = (AppSettingsXML) XMLSerialiser.Deserialize(stream);
                        this.ClassListToggle.IsChecked = settings.ShowClassListByDefault;
                        ThemesController.SetTheme((ThemeType) settings.Theme);
                    }
                }
            }
            catch (Exception e) {
                MessageBox.Show("Failed to load application config file at " + ConfigFile + "\n" + e, "Error loading config");
            }
        }

        public void SaveXML() {
            try {
                if (!Directory.Exists(ConfigFolder)) {
                    Directory.CreateDirectory(ConfigFolder);
                }

                using (BufferedStream stream = new BufferedStream(File.OpenWrite(ConfigFile))) {
                    XMLSerialiser.Serialize(stream, new AppSettingsXML() {
                        Theme = (int) ThemesController.CurrentTheme,
                        ShowClassListByDefault = this.ClassListToggle.IsChecked == true
                    });
                }
            }
            catch (Exception e) {
                MessageBox.Show("Failed to load application config file at " + ConfigFile + "\n" + e, "Error loading config");
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            // Collection<BaseInstructionViewModel> instructions = ((ClassListViewModel) this.DataContext).SelectedClass.MethodList.SelectedMethod.CodeEditor.ByteCodeEditor.Instructions;
            // List<Instruction> instructionHandles = instructions.Select(i => i.Node).ToList();
            // 
            // StringWriter writer = new StringWriter();
            // JsonTextWriter jsonWriter = new JsonTextWriter(writer);
            // JsonSerializer serializer = new JsonSerializer();
            // serializer.Formatting = Formatting.Indented;
            // serializer.Serialize(jsonWriter, instructionHandles);
            // 
            // Dialog.Message.ShowWarningDialog("Tetx", writer.ToString());
        }
    }
}