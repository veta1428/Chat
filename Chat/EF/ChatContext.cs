using Chat.EF.Configuration;
using Chat.EF.Entities;
using Microsoft.EntityFrameworkCore;
using CoreChat = Chat.EF.Entities.Chat;

namespace Chat.EF
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }

        public DbSet<CoreChat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<TextMessageContent> TextMessageContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<CoreChat>(new ChatConfiguration());
            modelBuilder.ApplyConfiguration<ChatUser>(new ChatUserConfiguration());
            modelBuilder.ApplyConfiguration<Message>(new MessageConfiguration());
            modelBuilder.ApplyConfiguration<MessageContent>(new MessageContentConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
        }
    }
}
