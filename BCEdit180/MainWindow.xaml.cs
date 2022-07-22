﻿using System.Diagnostics;
using System.IO;
using System.Windows;
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
    }
}