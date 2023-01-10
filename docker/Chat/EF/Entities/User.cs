namespace Chat.EF.Entities
{
    public class User : Entity<int>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public IEnumerable<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();

        public string FullName => FirstName + " " + LastName;

        public int IdentityId { get; set; }
    }
}
