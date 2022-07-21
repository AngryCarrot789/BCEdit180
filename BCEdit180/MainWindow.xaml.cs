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
            string path = "F:\\IJProjects\\CarrotTools\\out\\production\\CarrotTools\\reghzy\\carrottools\\playerdata\\results\\custom\\tileentity\\TileEntityTimingResult.class";
            using (BufferedStream stream = new BufferedStream(File.OpenRead(path))) {
                ClassNode node = ClassFile.ParseClass(stream);
                this.DataContext = new ClassViewModel(node);
            }


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
    }
}
