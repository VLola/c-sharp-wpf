using System.Net.Sockets;

namespace Project_64_Server.Model
{
    internal class ClientSocket
    {
        public string? FirstName { get; set; }
        public Socket? Socket { get; set; }
        public ClientSocket() { }
        public ClientSocket(string? firstName, Socket? socket)
        {
            FirstName = firstName;
            Socket = socket;
        }
    }
}
