using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using BCEdit180.Core;
using BCEdit180.Core.AttributeEditor;
using BCEdit180.Core.AttributeEditor.Classes;
using BCEdit180.Core.Dialogs;
using BCEdit180.Core.ViewModels;
using BCEdit180.Dialogs;

namespace BCEdit180 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            ServiceManager.SetService<IDialogManager>(new WindowsDialogs());
            ServiceManager.SetService<ITypeEditors>(new WindowsTypeEditors());
            ServiceManager.SetService<IAccessEditor>(new WindowsAccessEditor());
            ServiceManager.SetService<IFileDialog>(new WindowsFileDialogs());
            ServiceManager.SetService<IAppServices>(new WPFAppServices());
            this.DataContext = new ClassViewModel();

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
                    ((ClassViewModel) this.DataContext).ReadClassFileAndShowDialog(targetFile);
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
    }
}