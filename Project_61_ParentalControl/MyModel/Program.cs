namespace Project_61_ParentalControl.MyModel
{
    public class Program
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public Program(string Name, string FullName)
        {
            this.Name = Name;
            this.FullName = FullName;
        }
    }
}
