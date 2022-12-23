namespace Chat.Models
{
    public class ChatListModel
    {
        public ChatListModel(IEnumerable<ChatModel> chatList)
        {
            ChatList = chatList;
        }
        public IEnumerable<ChatModel> ChatList { get; set; } = new List<ChatModel>();
    }
}
