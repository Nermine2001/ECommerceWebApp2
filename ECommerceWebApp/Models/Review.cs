namespace ECommerceWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public string UserAvatar {  get; set; }
        public int Rate { get; set; }
        public string Content { get; set; }
        public List<ReviewReply> reviewReplies { get; set; } = new List<ReviewReply>();
    }
}