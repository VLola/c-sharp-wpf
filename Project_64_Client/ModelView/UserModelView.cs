using Project_64_Client.Command;
using Project_64_Client.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Project_64_Client.ModelView
{
    public class UserModelView
    {
        public ObservableCollection<UserModel> Users { get; set; } = new();
        public SelectedUserModel SelectedUser { get; set; } = new();
        private RelayCommand? _sendMessageCommand;
        public RelayCommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand ??
                  (_sendMessageCommand = new RelayCommand(obj =>
                  {
                      string message = SelectedUser.Message;
                      if (SelectedUser.User != null && message != "")
                      {
                          SelectedUser.User.Messages.Add(message);
                          SelectedUser.Message = "";
                      }
                  }));
            }
        }

        private RelayCommand? _addUserCommand;
        public RelayCommand AddUserCommand
        {
            get
            {
                return _addUserCommand ??
                  (_addUserCommand = new RelayCommand(obj =>
                  {
                      string name = SelectedUser.AddNameUser;
                      if (!CheckUser(name) && name != "")
                      {
                          Users.Add(new UserModel("", name));
                          SelectedUser.AddNameUser = "";
                      }
                      else MessageBox.Show("Error");
                  }));
            }
        }
        private bool CheckUser(string name)
        {
            foreach (var user in Users)
            {
                if (user.FirstName == name) return true;
            }
            return false;
        }
    }
}
