namespace Chat.Models
{
    public class MessageModel
    {
        public MessageModel(
            int sentByUserId, 
            string firstName, 
            string lastName, 
            DateTime sentDate, 
            string? text)
        {
            SentByUserId = sentByUserId;
            FirstName = firstName;
            LastName = lastName;
            SentDate = sentDate;
            Text = text;
        }

        public int SentByUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime SentDate { get; set; }

        public string? Text { get; set; }
    }
}
