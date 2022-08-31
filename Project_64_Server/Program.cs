using Project_64_Server.Model;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

string pathUsers = Directory.GetCurrentDirectory() + "/User";
string pathChats = Directory.GetCurrentDirectory() + "/Chats";
string pathStatusClient = Directory.GetCurrentDirectory() + "/StatusClient";
int chatId = 0;

ObservableCollection<ClientSocket> CollectionClientSoket = new();

try
{
    if (!Directory.Exists(pathUsers)) Directory.CreateDirectory(pathUsers);
    if (!Directory.Exists(pathStatusClient)) Directory.CreateDirectory(pathStatusClient);
    if (!Directory.Exists(pathChats)) Directory.CreateDirectory(pathChats);
    Listen();
    Console.Read();
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
            try {
                int bytes = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                do
                {
                    bytes = clientSocket.Receive(buffer);
                    builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (clientSocket.Available > 0);

                if (builder.ToString() == "")
                {
                    client.IsConnected = false;
                    ConnectedUser(client);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
                else
                {
                    ClientData? clientData = JsonSerializer.Deserialize<ClientData>(builder.ToString());
                    if (clientData != null)
                    {
                        if (clientData.IsLogin && clientData.FirstName != null && clientData.FirstName != "" && clientData.Password != null && clientData.Password != "")
                        {
                            if (LoginUser(clientData.FirstName, clientData.Password))
                            {
                                clientData.Login = true;
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                                client.FirstName = clientData.FirstName;
                                client.IsConnected = true;
                                ConnectedUser(client);
                                AddClientSocket(clientData.FirstName, clientSocket);
                            }
                            else
                            {
                                clientData.Login = false;
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                            }
                        }
                        else if (clientData.IsRegister && clientData.FirstName != null && clientData.FirstName != "" && clientData.Password != null && clientData.Password != "")
                        {
                            if (RegistrationUser(clientData.FirstName, clientData.Password))
                            {
                                clientData.Login = true;
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                                client.FirstName = clientData.FirstName;
                                client.IsConnected = true;
                                ConnectedUser(client);
                                AddClientSocket(clientData.FirstName, clientSocket);
                            }
                            else
                            {
                                clientData.Login = false;
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                            }
                        }
                        else if (clientData.IsFriendChat && clientData.FriendChat != null && clientData.FriendChat.FirstNameFriend != null && clientData.FirstName != null && clientData.FirstName != "")
                        {
                            if (clientData.FriendChat.ChatId == null)
                            {
                                string friendName = clientData.FriendChat.FirstNameFriend;
                                ChatId chat = new ChatId() { Id = chatId++, Names = new string[2] { clientData.FirstName, friendName } };
                                clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                                File.WriteAllText(pathChats + "/" + chatId, JsonSerializer.Serialize(chat));
                                clientData.FriendChat.FirstNameFriend = clientData.FirstName;
                                AddChatFriend(friendName, clientData);
                            }
                        }
                        else if (clientData.IsMessage && clientData.Message != null && clientData.FirstName != null && clientData.Message.FirstName != null)
                        {
                            AddMessageFriend(clientData.Message.FirstName, clientData);
                            clientData.FirstName = clientData.Message.FirstName;
                            clientSocket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
                        }
                    }
                }
            } catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    });
}

void AddMessage(ClientData clientData, Socket socket)
{
    socket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
}

void AddClientSocket(string firstName, Socket socket)
{
    if (!CheckClientSocket(firstName)) CollectionClientSoket.Add(new ClientSocket(firstName, socket));
    else
    {
        foreach (var item in CollectionClientSoket)
        {
            if (item.FirstName == firstName) item.Socket = socket;
        }
    }
}

bool CheckClientSocket(string firstName)
{
    foreach (var item in CollectionClientSoket)
    {
        if (item.FirstName == firstName) return true;
    }
    return false;
}

void AddChatFriend(string firstName, ClientData clientData)
{
    foreach (var item in CollectionClientSoket)
    {
        if (item.FirstName == firstName) item.Socket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
    }
}

void AddMessageFriend(string firstName, ClientData clientData)
{
    try
    {
        foreach (var item in CollectionClientSoket)
        {
            if (item.FirstName == firstName) item.Socket.Send(Encoding.Unicode.GetBytes(JsonSerializer.Serialize(clientData)));
        }
    } catch { }
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
        if (user != null && Crypt.Veryfy(password, user.Password)) return true;
        else return false;
    }
    else return false;
}

void ConnectedUser(Client client)
{
    if(client.FirstName != null) File.WriteAllText(pathStatusClient + "/" + client.FirstName, JsonSerializer.Serialize(client));
}
