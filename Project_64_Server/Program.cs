using Project_64_Server.Model;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

string pathUsers = Directory.GetCurrentDirectory() + "/User";
string pathStatusClient = Directory.GetCurrentDirectory() + "/StatusClient";

ObservableCollection<Client> Collection = new ObservableCollection<Client>();

try
{
    if (!Directory.Exists(pathUsers)) Directory.CreateDirectory(pathUsers);
    if (!Directory.Exists(pathStatusClient)) Directory.CreateDirectory(pathStatusClient);
    Listen();
}
catch (Exception ex) {
    Console.WriteLine(ex.Message);
}

void Listen()
{
    Task.Run(() => {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8086);
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(endPoint);
        serverSocket.Listen(10);
        while (true)
        {
            Connect(serverSocket.Accept());
        }
    });
}

void Connect(Socket clientSocket)
{
    Task.Run(() => {
        Client client = new Client();
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

            if (builder.ToString() == "") {
                client.IsConnected = false;
                ConnectedUser(client);
            }
            else
            {
                ClientData? clientData = JsonSerializer.Deserialize<ClientData>(builder.ToString());
                if (clientData != null)
                    if (clientData.IsLogin == true)
                    {
                        if (LoginUser(clientData.FirstName, clientData.Password))
                        {
                            byte[] data = Encoding.Unicode.GetBytes("ok");

                            clientSocket.Send(data); 
                            client.FirstName = clientData.FirstName;
                            client.IsConnected = true;
                            ConnectedUser(client);
                        }
                        else
                        {
                            byte[] data = Encoding.Unicode.GetBytes("login error");
                            clientSocket.Send(data);
                        }
                    }
                    else if (clientData.IsRegister == true)
                    {
                        if (RegistrationUser(clientData.FirstName, clientData.Password))
                        {
                            byte[] data = Encoding.Unicode.GetBytes("ok");
                            clientSocket.Send(data);
                            client.FirstName = clientData.FirstName;
                            client.IsConnected = true;
                            ConnectedUser(client);
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

bool RegistrationUser(string firstName, string password)
{
    if (File.Exists(pathUsers + "/" + firstName)) return false;
    else
    {
        File.WriteAllText(pathUsers + "/" + firstName, JsonSerializer.Serialize(new User(firstName, Crypt.Generate(password))));
        return true;
    }
}

bool LoginUser(string firstName, string password)
{
    if (File.Exists(pathUsers + "/" + firstName))
    {
        User? user = JsonSerializer.Deserialize<User>(File.ReadAllText(pathUsers + "/" + firstName));
        if (user != null && user.Password == Crypt.Generate(password)) return true;
        else return false;
    }
    else return false;
}

void ConnectedUser(Client client)
{
    if(client.FirstName != null && client.FirstName != null) File.WriteAllText(pathStatusClient + "/" + client.FirstName, JsonSerializer.Serialize(client));
}
