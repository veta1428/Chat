using System.Security.Claims;
using AuthServer.Data;
using AuthServer.EF;
using AuthServer.Entities;
using AuthServer.Options;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task InitializeData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<AuthServerContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var oidcOptions = serviceScope.ServiceProvider.GetRequiredService<IOptions<ChatOidcOptions>>();
                if (!context.Clients.Any())
                {
                    foreach (var client in Clients.Get(oidcOptions.Value))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var scope in Resources.GetApiScopes())
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Resources.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (!userManager.Users.Any())
                {
                    foreach (var testUser in Users.Get())
                    {

                        var user = new User
                        {
                            FirstName = testUser.Claims.First(c => c.Type == JwtClaimTypes.GivenName).Value,
                            LastName = testUser.Claims.First(c => c.Type == JwtClaimTypes.FamilyName).Value,
                            Email = testUser.Claims.First(c => c.Type == JwtClaimTypes.Email).Value,
                            UserName = testUser.Claims.First(c => c.Type == JwtClaimTypes.Email).Value
                        };

                        var result = await userManager.CreateAsync(user, testUser.Password);

                        if (!result.Succeeded)
                            throw new Exception("Failed to create user!");

                        await userManager.AddClaimsAsync(user, testUser.Claims.Select(c => new Claim(c.Type, c.Value)).ToList());
                    }
                }
            }
        }
    }
}
