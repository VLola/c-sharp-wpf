using Newtonsoft.Json;
using Project_63_Server.Model;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_63_Server
{
    public partial class MainWindow : Window
    {
        private int ClientId = 0;
        public ObservableCollection<ConnectClient> Collection { get; set; } = new ObservableCollection<ConnectClient>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            this.DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Listen();
        }

        private void Listen()
        {
            Task.Run(() => {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086);
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);
                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();
                    Connect(clientSocket, ClientId++);
                }
            });
        }
        private void Connect(Socket clientSocket, int id)
        {
            Task.Run(() => {

                ConnectClient connectClient = new ConnectClient();
                connectClient.Id = id;
                Dispatcher.Invoke(new Action(() => {
                    Collection.Add(connectClient);
                }));
                while (true)
                {
                    int bytes = 0;
                    byte[] buffer = new byte[1024];
                    StringBuilder builder = new StringBuilder();
                    do
                    {
                        bytes = clientSocket.Receive(buffer);
                        builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                    } while (clientSocket.Available > 0);

                    if(builder.ToString() == "") Remove(id);
                    else
                    {
                        Client client = JsonConvert.DeserializeObject<Client>(builder.ToString());

                        User user = new User();
                        user.Email = client.Email;
                        user.Password = client.Password;
                        if (client.IsLogin == true)
                        {
                            if (ConnectDB.LoginUser(user.Email, user.Password))
                            {
                                byte[] data = Encoding.Unicode.GetBytes("login ok");
                                clientSocket.Send(data);
                                Login(user.Email, id);
                            }
                            else
                            {
                                byte[] data = Encoding.Unicode.GetBytes("login error");
                                clientSocket.Send(data);
                            }
                        }
                        else if (client.IsRegister == true)
                        {
                            if (ConnectDB.RegistrationUser(user.Email, user.Password))
                            {
                                byte[] data = Encoding.Unicode.GetBytes("reg ok");
                                clientSocket.Send(data);
                            }
                            else
                            {
                                byte[] data = Encoding.Unicode.GetBytes("reg error");
                                clientSocket.Send(data);
                            }
                        }
                    }
                    
                }
                //clientSocket.Shutdown(SocketShutdown.Both);
                //clientSocket.Close();
            });
        }
        public void Login(string email, int id)
        {
            Dispatcher.Invoke(new Action(() => {
                for (int i = 0; i < Collection.Count; i++)
                    if (Collection[i].Id == id)
                    {
                        Collection.RemoveAt(i);
                        Collection.Add(new ConnectClient(id, email));
                        break;
                    }
            }));
        }
        public void Remove(int id)
        {
            Dispatcher.Invoke(new Action(() => {
                for(int i = 0; i < Collection.Count; i++)
                    if (Collection[i].Id == id)
                    {
                        Collection.RemoveAt(i);
                        break;
                    }
            }));
        }
        public class ConnectClient
        {
            public int Id { get; set; }
            public string Email { get; set; } = "No login";
            public ConnectClient() { }
            public ConnectClient(int Id, string Email) {
                this.Id = Id;
                this.Email = Email;
            }
        }
    }
}
