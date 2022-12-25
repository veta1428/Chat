﻿using AuthServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureIdentityServer4(this IServiceCollection services, IConfiguration configuration, string assemblyName)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var identityServerBuilder = services.AddIdentityServer(options =>
            {
                options.Authentication.CheckSessionCookieName = "chat.auth";

                options.UserInteraction.LoginUrl = "/membership/login";
                //options.UserInteraction.ErrorUrl = "/account";
                options.UserInteraction.LogoutUrl = "/membership/logout";
                //options.UserInteraction.Register

                //if (!string.IsNullOrEmpty(Configuration["Issuer"]))
                //{
                //    options.IssuerUri = Configuration["Issuer"];
                //}
            })

            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                                b.UseSqlServer(
                                    connectionString,
                                    sql => sql.MigrationsAssembly(assemblyName));
            })

            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(
                        connectionString,
                        sql => sql.MigrationsAssembly(assemblyName));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            })
            //.AddMessageStores()
            .AddAspNetIdentity<User>()
            .AddDeveloperSigningCredential();

            // for correct logout
            services.AddAuthentication(options =>
            {
                options.DefaultSignOutScheme = options.DefaultAuthenticateScheme;
            });
        }
    }
}
