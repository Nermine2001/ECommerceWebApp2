namespace ECommerceWebApp.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Profession { get; set; }
        public string ImageUrl { get; set; }
        public string Feedback { get; set; }
        public int Rating { get; set; }
    }
}
