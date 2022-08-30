using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_64_Client.Model
{
    public class UserModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public bool IsLogin { get; set; }
        public ObservableCollection<string> Messages { get; set; } = new();
        public UserModel(string email, string name)
        {
            Email = email;
            FirstName = name;
        }
    }
}
