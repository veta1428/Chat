namespace Chat.EF.Entities
{
    public class Chat : Entity<int>
    {
        public DateTime CreatedDateTime { get; set; }

        public string? Name { get; set; }

        public IEnumerable<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();

        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    }
}
