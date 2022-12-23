namespace Chat.EF.Entities
{
    public class ChatUser : Entity<int>
    {
        public User? User { get; set; }

        public int UserId { get; set; }

        public Chat? Chat { get; set; }

        public int ChatId { get; set; }
    }
}
