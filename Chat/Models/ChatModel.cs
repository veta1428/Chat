namespace Chat.Models
{
    public class ChatModel
    {
        public ChatModel(int id, string title, DateTime createdDateTime)
        {
            Id = id;
            CreatedDateTime = createdDateTime;
            Title = title;
        }

        public int Id { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string? Title { get; set; }
    }
}
