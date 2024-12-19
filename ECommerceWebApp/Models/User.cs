namespace ECommerceWebApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone {  get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public User()
        {
            
        }

        public User(string id, string name, string email, string password, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
