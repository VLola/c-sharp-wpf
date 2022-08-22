namespace Project_63_Server.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Client() { }
        public Client(int Id, string Email)
        {
            this.Id = Id;
            this.Email = Email;
        }
    }
}
