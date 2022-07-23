using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using BCEdit180.CodeEditing;
using BCEdit180.ViewModels;
using JavaAsm;
using JavaAsm.IO;

namespace BCEdit180 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // string path = "F:\\IJProjects\\CarrotTools\\out\\production\\CarrotTools\\reghzy\\carrottools\\playerdata\\results\\custom\\tileentity\\TileEntityTimingResult.class";
            // string path = "F:\\MinecraftUtils\\server\\tekkitmain-1.6.4\\CarrotClassSplicer\\World.class";
            string path = @"F:\IJProjects\CarrotTools\out\production\CarrotTools\reghzy\carrottools\playerdata\results\custom\BlockLookupResult.class";
            using (BufferedStream stream = new BufferedStream(File.OpenRead(path))) {
                ClassNode node = ClassFile.ParseClass(stream);
                this.DataContext = new ClassViewModel(node) {
                    FilePath = path
                };
            }

            // was planning on using a java server to process classfiles, because the asm-all library is pretty good
            // but then i found java-asm, which seems like the exact same as asm-all, except in c# :))
            // (you can download my fork of it, which i downgraded to .NET standard 2.0)

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

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            ClassViewModel vm = (ClassViewModel) this.DataContext;

            if (File.Exists(vm.FilePath)) {
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

                string targetFile = null;
                foreach (string path in files) {
                    if (path.EndsWith(".class")) {
                        if (File.Exists(path)) {
                            targetFile = path;
                            break;
                        }
                    }
                }

                if (targetFile != null) {
                    ((ClassViewModel) this.DataContext).ReadClassFile(targetFile);
                }
                else {
                    e.Handled = true;
                }
            }
        }

        // i have literally no idea how to do this using only MVVM

        private void RemoveSelectedMethodsClick(object sender, RoutedEventArgs e) {
            if (this.MethodList.SelectedItems.Count == 0) {
                return;
            }

            List<MethodInfoViewModel> remove = new List<MethodInfoViewModel>();
            foreach(object element in this.MethodList.SelectedItems) {
                if (element != null) {
                    remove.Add((MethodInfoViewModel) element);
                }
            }

            if (remove.Count == 0) {
                return;
            }

            CodeEditorViewModel editor = remove[0].CodeEditor;
            ObservableCollection<MethodInfoViewModel> actualMethods = editor.MethodInfo.MethodList.Methods;
            ObservableCollection<MethodInfoViewModel> removedMethods = editor.MethodInfo.MethodList.RemovedMethods;
            foreach (MethodInfoViewModel method in remove) {
                actualMethods.Remove(method);
                removedMethods.Add(method);
            }
        }

        private void RemoveAddedMethod(object sender, RoutedEventArgs e) {
            if (this.AddedMethodList.SelectedItems.Count == 0) {
                return;
            }

            List<MethodInfoViewModel> remove = new List<MethodInfoViewModel>();
            foreach (object element in this.AddedMethodList.SelectedItems) {
                if (element != null) {
                    remove.Add((MethodInfoViewModel) element);
                }
            }

            if (remove.Count == 0) {
                return;
            }

            CodeEditorViewModel editor = remove[0].CodeEditor;
            ObservableCollection<MethodInfoViewModel> methods = editor.MethodInfo.MethodList.AddedMethods;
            ObservableCollection<MethodInfoViewModel> removedMethods = editor.MethodInfo.MethodList.RemovedMethods;
            foreach (MethodInfoViewModel method in remove) {
                methods.Remove(method);
                removedMethods.Add(method);
            }
        }

        private void UndoRemoveMethod(object sender, RoutedEventArgs e) {
            if (this.RemovedMethodList.SelectedItems.Count == 0) {
                return;
            }

            List<MethodInfoViewModel> removed = new List<MethodInfoViewModel>();
            foreach (object element in this.RemovedMethodList.SelectedItems) {
                if (element != null) {
                    removed.Add((MethodInfoViewModel) element);
                }
            }

            if (removed.Count == 0) {
                return;
            }

            CodeEditorViewModel editor = removed[0].CodeEditor;
            ObservableCollection<MethodInfoViewModel> removedMethods = editor.MethodInfo.MethodList.RemovedMethods;
            ObservableCollection<MethodInfoViewModel> actualMethods = editor.MethodInfo.MethodList.Methods;
            foreach (MethodInfoViewModel method in removed) {
                removedMethods.Remove(method);
                actualMethods.Add(method);
            }
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {

        }
    }
}
