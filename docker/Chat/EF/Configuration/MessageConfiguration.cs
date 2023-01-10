using Chat.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.EF.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(m => m.UserFrom)
                .WithMany()
                .HasForeignKey(m => m.UserFromId);

            builder
                .HasOne(m => m.Chat)
                .WithMany(chat => chat.Messages)
                .HasForeignKey(m => m.ChatId);
        }
    }
}
