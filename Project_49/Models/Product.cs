namespace Project_49.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
