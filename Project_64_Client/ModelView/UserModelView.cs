using Project_64_Client.Command;
using Project_64_Client.Model;
using Project_64_Client.Object;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project_64_Client.ModelView
{
    public class UserModelView
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback
        );
        private PasswordBox _passwordBox;
        private Socket? _socket;
        public ClientModel ClientModel { get; set; } = new();
        public ObservableCollection<UserModel> Users { get; set; } = new();
        public SelectedUserModel SelectedUser { get; set; } = new();
        private RelayCommand? _sendMessageCommand;
        private RelayCommand? _addUserCommand;
        private RelayCommand? _connectCommand;
        private RelayCommand? _disconnectCommand;
        private RelayCommand? _startRecordCommand;
        private RelayCommand? _stopRecordCommand;
        public UserModelView(PasswordBox passwordBox)
        {
            _passwordBox = passwordBox;
            Load();
            Listen();
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
        public RelayCommand StartRecordCommand
        {
            get { return _startRecordCommand ?? (_startRecordCommand = new RelayCommand(obj => { StartRecord(); })); }
        }
        public RelayCommand StopRecordCommand
        {
            get { return _stopRecordCommand ?? (_stopRecordCommand = new RelayCommand(obj => { StopRecord(); })); }
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
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        private void Reload()
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Users.Clear();
            }));
        }
        private void AddUser()
        {
            string friendName = SelectedUser.AddNameUser;
            if (!CheckUser(friendName) && friendName != "")
            {
                ClientData clientData = new() { FirstName = ClientModel.FirstName, IsFriendChat = true, FriendChat = new() { FirstNameFriend = friendName } };
                Send(clientData);
                SelectedUser.AddNameUser = "";
            }
            else MessageBox.Show("Error");
        }
        private void SendMessage()
        {
            string message = SelectedUser.Message;
            if (SelectedUser.User != null && message != "")
            {
                ChatMessage chatMessage = new ChatMessage() { FirstName = SelectedUser.User.FirstName, Message = message };
                ClientData clientData = new() { FirstName = ClientModel.FirstName, IsMessage = true, Message = chatMessage };
                Send(clientData);
                SelectedUser.Message = "";
            }
        }
        private void StartRecord()
        {
            ClientModel.IsRecord = true;
            mciSendString("open new type WAVEAudio alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
        }
        private void StopRecord()
        {
            mciSendString("stop recsound", "", 0, 0);
            mciSendString("save recsound temp.wav", "", 0, 0);
            mciSendString("close recsound", "", 0, 0);
            ClientModel.IsRecord = false;
            string message = DateTime.Now.ToString();
            if (SelectedUser.User != null)
            {
                ChatMessage chatMessage = new ChatMessage() { 
                    FirstName = SelectedUser.User.FirstName,
                    Message = message, IsRecord = true, 
                    Record = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/temp.wav") 
                };
                ClientData clientData = new() { 
                    FirstName = ClientModel.FirstName, 
                    IsMessage = true, 
                    Message = chatMessage 
                };
                Send(clientData);
                SelectedUser.Message = "";
            }
        }
        private void Connect()
        {
            ClientData clientData = new();
            clientData.FirstName = ClientModel.FirstName;
            clientData.Password = _passwordBox.Password;
            if (ClientModel.IsRegister == true)
            {
                clientData.IsRegister = true;
                Send(clientData);
            }
            else if (ClientModel.IsLogin == true)
            {
                clientData.IsLogin = true;
                Send(clientData);
            }
        }
        private void Disconnect()
        {
            ClientModel.IsLoginClient = false;
            Close();
            Reload();
            Load();
            Listen();
        }
        private void Send(ClientData clientData)
        {
            if (_socket != null)
            {
                _socket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
            }
        }
        private void Listen()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if(_socket != null)
                    {
                        Recive();
                    }
                }
            });
        }
        private void Recive()
        {
            try
            {
                int bytes = 0;
                byte[] buffer = new byte[64000];
                StringBuilder builder = new StringBuilder();
                do
                {
                    bytes = _socket.Receive(buffer);
                } while (_socket.Available > 0);

                builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));

                ClientData? clientData = JsonSerializer.Deserialize<ClientData>(builder.ToString());
                if (clientData != null)
                {
                    if (clientData.IsLogin)
                    {
                        if (clientData.Login)
                        {
                            Repassword("");
                            ClientModel.IsLoginClient = true;
                        }
                        else
                        {
                            Repassword("");
                            MessageBox.Show("Error login");
                        }
                    }
                    else if (clientData.IsRegister)
                    {
                        if (clientData.Login)
                        {
                            Repassword("");
                            ClientModel.IsLoginClient = true;
                        }
                        else
                        {
                            Repassword("");
                            MessageBox.Show("Error register");
                        }
                    }
                    else if (clientData.IsFriendChat && clientData.FriendChat != null && clientData.FriendChat.FirstNameFriend != null)
                    {
                        if (clientData.FriendChat.Messages == null) LoadCollectionUser(new UserModel(clientData.FriendChat.FirstNameFriend));
                        else LoadCollectionUser(new UserModel(clientData.FriendChat.FirstNameFriend) { Messages = clientData.FriendChat.Messages });
                    }
                    else if (clientData.IsMessage)
                    {
                        if (clientData.Message?.FirstName == ClientModel.FirstName) clientData.Message.IsMyMessage = true;
                        LoadCollectionMessage(clientData.FirstName, clientData.Message);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void LoadCollectionUser(UserModel userModel)
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Users.Add(userModel);
            }));
        }
        private void LoadCollectionMessage(string firstName, ChatMessage chatMessage)
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                foreach (var user in Users)
                {
                    if (user.FirstName == firstName) user.AddMessage(chatMessage);
                }
            }));
        }
        private void Repassword(string password)
        {
            _passwordBox.Dispatcher.Invoke(new System.Action(() =>
            {
                _passwordBox.Password = password;
            }));
        }
    }
}
