namespace Project_64_Server.Model
{
    public class Client
    {
        public string FirstName { get; set; }
        public bool IsConnected { get; set; }
        public Client() { }
        public Client(string firstName, bool isConnected)
        {
            FirstName = firstName;
            IsConnected = isConnected;
        }
    }
}
