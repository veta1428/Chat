using Chat.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.EF.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany<ChatUser>(c => c.ChatUsers)
                .WithOne(cu => cu.User)
                .HasForeignKey(cu => cu.UserId);

            builder.HasData(new User[]
            {
                new User (){ Id = 1, FirstName = "Alice", LastName = "A"},
                new User (){ Id = 2, FirstName = "Bob", LastName = "B"},
                new User (){ Id = 3, FirstName = "Sam", LastName = "S"}
            });
        }
    }
}
