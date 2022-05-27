namespace Project_49.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Latest { get; set; }
        [Required]
        public string Image { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public bool? Availability { get; set; }
    }
}
