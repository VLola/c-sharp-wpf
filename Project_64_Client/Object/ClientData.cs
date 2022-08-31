namespace Project_64_Client.Object
{
    internal class ClientData
    {
        public string? FirstName { get; set; }
        public string? Password { get; set; }
        public bool IsLogin { get; set; }
        public bool Login { get; set; }
        public bool IsRegister { get; set; }
        public bool IsMessage { get; set; }
        public ChatMessage? Message { get; set; }
        public bool IsFriendChat { get; set; }
        public Chat? FriendChat { get; set; }
        public bool IsRecord { get; set; }
        public bool Record { get; set; }
        public ClientData() { }
    }
}
