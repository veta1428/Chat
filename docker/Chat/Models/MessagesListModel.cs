namespace Chat.Models
{
    public class MessagesListModel
    {
        public MessagesListModel(IEnumerable<MessageModel> messageModels)
        {
            MessageModels = messageModels;
        }

        public IEnumerable<MessageModel> MessageModels { get; set; }
    }
}
