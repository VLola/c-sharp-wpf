using System.Collections.ObjectModel;

namespace Project_64_Server.Object
{
    public class Chat
    {
        public int? ChatId { get; set; }
        public string? FirstNameFriend { get; set; }
        public ObservableCollection<ChatMessage>? Messages { get; set; }
    }
}
