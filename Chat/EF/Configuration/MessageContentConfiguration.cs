using Chat.EF.Entities;
using Chat.EF.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.EF.Configuration
{
    public class MessageContentConfiguration : IEntityTypeConfiguration<MessageContent>
    {
        public void Configure(EntityTypeBuilder<MessageContent> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasDiscriminator<MessageContentType>(x => x.Type)
                .HasValue<TextMessageContent>(MessageContentType.TextMessage);
        }
    }
}
