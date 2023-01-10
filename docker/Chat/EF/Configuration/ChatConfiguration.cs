using Chat.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.EF.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Entities.Chat>
    {
        public void Configure(EntityTypeBuilder<Entities.Chat> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany<ChatUser>(c => c.ChatUsers)
                .WithOne(cu => cu.Chat)
                .HasForeignKey(cu => cu.ChatId);

            builder
                .HasMany(chat => chat.Messages)
                .WithOne(m => m.Chat);
        }
    }
}
