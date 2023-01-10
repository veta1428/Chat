namespace Chat.Models
{
    public class CreateChatModel
    {
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
    }
}
