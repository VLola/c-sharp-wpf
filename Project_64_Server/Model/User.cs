namespace Project_64_Server.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string FirstName, string Password) {
            this.FirstName = FirstName;
            this.Password = Password;
        }
    }
}
