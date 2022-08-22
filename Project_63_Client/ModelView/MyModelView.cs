using GalaSoft.MvvmLight.Command;
using Project_63_Client.Model;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Project_63_Client.ModelView
{
    public class MyModelView
    {
        public MyModel MyModel { get; set; } = new MyModel();
        private Socket _socket;
        private RelayCommand _connectCommand;
        private RelayCommand _disconnectCommand;
        PasswordBox passwordBox;
        public MyModelView(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;
            Load();
        }
        private void Load()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086));
        }
        public void Close()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        public RelayCommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new RelayCommand(Connect));
            }
        }
        public RelayCommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new RelayCommand(Disconnect));
            }
        }
        private void Connect()
        {
            Client client = new Client();
            client.Email = MyModel.Email;
            client.Password = passwordBox.Password;
            if (MyModel.IsRegister == true)
            {
                client.IsRegister = true;
                Send(client);
                Recive();
            }
            else if (MyModel.IsLogin == true)
            {
                client.IsLogin = true;
                Send(client);
                Recive();
            }
        }
        private void Disconnect()
        {
            MyModel.IsLoginClient = false;
            Close();
            Load();
        }
        private void Send(Client client)
        {
            byte[] data = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(client));
            _socket.Send(data);
        }
        private void Recive()
        {
            int bytes = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            do
            {
                bytes = _socket.Receive(buffer);
                builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            } while (_socket.Available > 0);
            string message = builder.ToString();
            if (message == "ok")
            {
                passwordBox.Password = "";
                MyModel.IsLoginClient = true;
            }
            else
            {
                passwordBox.Password = "";
                MessageBox.Show(message);
            }
        }
    }
}
