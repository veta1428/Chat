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

            builder
                .HasIndex(u => u.IdentityId)
                .IsUnique();

            builder.HasData(new User[]
            {
                new User (){ Id = 1, FirstName = "Jane", LastName = "Doe", IdentityId = 1},
                new User (){ Id = 2, FirstName = "User1", LastName = "Firstovich", IdentityId = 2 },
                new User (){ Id = 3, FirstName = "User2", LastName = "Secondovich", IdentityId = 3 }
            });
        }
    }
}
