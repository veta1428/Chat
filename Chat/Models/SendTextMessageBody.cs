namespace Chat.Models
{
    public class SendTextMessageBody
    {
        public SendTextMessageBody(int chatIdTo, string text)
        {
            ChatIdTo = chatIdTo;
            Text = text;
        }

        public int ChatIdTo { get; set; }

        public string Text { get; set; }
    }
}
