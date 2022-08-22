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
        private int ClientId = 1;
        public ObservableCollection<Client> Collection { get; set; } = new ObservableCollection<Client>();
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
                    Connect(serverSocket.Accept(), ClientId++);
                }
            });
        }
        private void Connect(Socket clientSocket, int id)
        {
            Task.Run(() => {
                SaveClientId(id);
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

                    if(builder.ToString() == "") RemoveClient(id);
                    else
                    {
                        ClientData clientData = JsonConvert.DeserializeObject<ClientData>(builder.ToString());

                        User user = new User();
                        user.Email = clientData.Email;
                        user.Password = clientData.Password;
                        if (clientData.IsLogin == true)
                        {
                            if (ConnectDB.LoginUser(user.Email, user.Password))
                            {
                                byte[] data = Encoding.Unicode.GetBytes("ok");
                                clientSocket.Send(data);
                                SaveClient(user.Email, id);
                            }
                            else
                            {
                                byte[] data = Encoding.Unicode.GetBytes("login error");
                                clientSocket.Send(data);
                            }
                        }
                        else if (clientData.IsRegister == true)
                        {
                            if (ConnectDB.RegistrationUser(user.Email, user.Password))
                            {
                                byte[] data = Encoding.Unicode.GetBytes("ok");
                                clientSocket.Send(data);
                            }
                            else
                            {
                                byte[] data = Encoding.Unicode.GetBytes("register error");
                                clientSocket.Send(data);
                            }
                        }
                    }
                }
            });
        }
        private void SaveClientId(int id)
        {
            Dispatcher.Invoke(new Action(() => {
                Collection.Add(new Client(id, "No login"));
            }));
        }
        private void SaveClient(string email, int id)
        {
            Dispatcher.Invoke(new Action(() => {
                for (int i = 0; i < Collection.Count; i++)
                    if (Collection[i].Id == id)
                    {
                        Collection.RemoveAt(i);
                        Collection.Add(new Client(id, email));
                        break;
                    }
            }));
        }
        private void RemoveClient(int id)
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
    }
}
