using System.ComponentModel.DataAnnotations;

namespace ECommerceWebApp.Models
{
    public class Product
    {
        public  int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Range(0, 10000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public bool IsFeatured { get; set; }
        public int Weight { get; set; }
        public string Origin { get; set; }
        public string Quality { get; set; }
        public string CheckStatus {  get; set; }
        public int MinWeight { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public int Rating { get; set; }
        public Product()
        {
            
        }

        public Product(int id, string name, string description, decimal price, string category, string imageURL)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            ImageURL = imageURL;
        }
    }
}
