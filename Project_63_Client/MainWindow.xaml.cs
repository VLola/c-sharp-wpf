using Project_63_Client.Model;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Project_63_Client
{
    public partial class MainWindow : Window
    {
        IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 8086);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, System.EventArgs e)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            socket.Connect(endPoint);
        }

        private void Connect()
        {
            Client client = new Client();
            client.Email = Email.Text;
            client.Password = Password.Text;
            if (IsRegister.IsChecked == true)
            {
                client.IsRegister = true;
                byte[] data = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(client));
                socket.Send(data);

                int bytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                do
                {
                    bytes = socket.Receive(buffer);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (socket.Available > 0);

                MessageBox.Show(builder.ToString());
            }
            else if (IsLogin.IsChecked == true)
            {
                client.IsLogin = true;
                byte[] data = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(client));
                socket.Send(data);

                int bytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                do
                {
                    bytes = socket.Receive(buffer);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (socket.Available > 0);

                MessageBox.Show(builder.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }
    }
}
