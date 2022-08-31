namespace Project_64_Client.Object
{
    public class ChatMessage
    {
        public string? FirstName { get; set; }
        public string? Message { get; set; }
        public bool IsMyMessage { get; set; }
        public bool IsRecord { get; set; }
        public byte[]? Record { get; set; }
    }
}
