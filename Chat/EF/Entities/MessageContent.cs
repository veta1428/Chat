using Chat.EF.Types;

namespace Chat.EF.Entities
{
    public abstract class MessageContent : Entity<int>
    {
        public MessageContentType Type { get; set; }
    }
}
