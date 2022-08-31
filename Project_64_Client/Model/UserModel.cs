using Project_64_Client.Object;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_64_Client.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public string FirstName { get; set; }
        public bool IsLogin { get; set; }
        private ObservableCollection<ChatMessage> _messages { get; set; } = new();
        public ObservableCollection<ChatMessage> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged("Messages");
            }
        }
        private ChatMessage _selectMessage { get; set; }
        public ChatMessage SelectMessage
        {
            get { return _selectMessage; }
            set
            {
                _selectMessage = value;
                OnPropertyChanged("SelectMessage");
            }
        }
        public UserModel() { }
        public UserModel(string name)
        {
            FirstName = name;
        }
        public void AddMessage(ChatMessage chatMessage)
        {
            Messages.Add(chatMessage);
        }
    }
}
