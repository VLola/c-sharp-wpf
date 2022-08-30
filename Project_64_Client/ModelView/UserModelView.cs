using Project_64_Client.Command;
using Project_64_Client.Model;
using Project_64_Client.Object;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Project_64_Client.ModelView
{
    public class UserModelView
    {
        private PasswordBox _passwordBox;
        private Socket? _socket;
        public ClientModel ClientModel { get; set; } = new();
        public ObservableCollection<UserModel> Users { get; set; } = new();
        public SelectedUserModel SelectedUser { get; set; } = new();
        private RelayCommand? _sendMessageCommand;
        private RelayCommand? _addUserCommand;
        private RelayCommand? _connectCommand;
        private RelayCommand? _disconnectCommand;
        public UserModelView(PasswordBox passwordBox)
        {
            _passwordBox = passwordBox;
        }
        public RelayCommand ConnectCommand
        {
            get { return _connectCommand ?? (_connectCommand = new RelayCommand(obj => { Connect(); })); }
        }
        public RelayCommand DisconnectCommand
        {
            get { return _disconnectCommand ?? (_disconnectCommand = new RelayCommand(obj => { Disconnect(); })); }
        }
        public RelayCommand SendMessageCommand
        {
            get { return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(obj => { SendMessage(); })); }
        }
        public RelayCommand AddUserCommand
        {
            get { return _addUserCommand ?? (_addUserCommand = new RelayCommand(obj => { AddUser(); })); }
        }
        private bool CheckUser(string name)
        {
            foreach (var user in Users)
            {
                if (user.FirstName == name) return true;
            }
            return false;
        }

        private void Load()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086));
        }
        public void Close()
        {
            if(_socket != null)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
        private void AddUser()
        {
            string name = SelectedUser.AddNameUser;
            if (!CheckUser(name) && name != "")
            {
                Users.Add(new UserModel("", name));
                SelectedUser.AddNameUser = "";
            }
            else MessageBox.Show("Error");
        }
        private void SendMessage()
        {
            string message = SelectedUser.Message;
            if (SelectedUser.User != null && message != "")
            {
                SelectedUser.User.Messages.Add(message);
                SelectedUser.Message = "";
            }
        }
        private void Connect()
        {
            Client client = new Client();
            client.Email = ClientModel.Email;
            client.Password = _passwordBox.Password;
            if (ClientModel.IsRegister == true)
            {
                client.IsRegister = true;
                Send(client);
                Recive();
            }
            else if (ClientModel.IsLogin == true)
            {
                client.IsLogin = true;
                Send(client);
                Recive();
            }
        }
        private void Disconnect()
        {
            ClientModel.IsLoginClient = false;
            Close();
            Load();
        }
        private void Send(Client client)
        {
            if (_socket != null)
            {
                byte[] data = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(client));
                _socket.Send(data);
            }
        }
        private void Recive()
        {
            if (_socket != null)
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
                    _passwordBox.Password = "";
                    ClientModel.IsLoginClient = true;
                }
                else
                {
                    _passwordBox.Password = "";
                    MessageBox.Show(message);
                }
            }
        }
    }
}
