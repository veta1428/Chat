namespace Chat.Models
{
    public class CreateChatModel
    {
        public string? Name { get; set; }
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
    }
}
