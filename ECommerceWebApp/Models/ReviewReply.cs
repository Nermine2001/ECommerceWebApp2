namespace ECommerceWebApp.Models
{
    public class ReviewReply
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public string UserAvatar { get; set; }  
        public DateTime Date { get; set; }   
    }
}