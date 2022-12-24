using AuthServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.EF
{
    public class AuthServerContext : IdentityDbContext<User>
    {
        public AuthServerContext(DbContextOptions<AuthServerContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
