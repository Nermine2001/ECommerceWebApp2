namespace ECommerceWebApp.Models
{
    public class ShopViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string Category { get; internal set; }
    }
}
