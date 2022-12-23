namespace Chat.EF.Entities
{
    public class Message : Entity<int>
    {
        public User? UserFrom { get; set; }

        public int UserFromId { get; set; }

        public int ChatId { get; set; }

        public Chat? Chat { get; set; }

        public DateTime SentDateTime { get; set; }

        public int MessageContentId { get; set; }

        public MessageContent? MessageContent { get; set; }
    }
}
