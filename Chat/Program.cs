using Chat.EF;
using Microsoft.EntityFrameworkCore;
using Chat.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Chat.Services;
using Chat.Options;

namespace Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Add services to the container.
            builder.Services.AddDbContext<ChatContext>(x =>
            {
                x.UseSqlServer(connectionString);
            });
            //builder.Services.AddCors();
            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddOptions();
            builder.Services.Configure<AuthorityOptions>(builder.Configuration.GetSection("AuthorityOptions"));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserAccessor, UserAccessor>();

            builder.Services.ConfigureAuthorization(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("index.html");

            if (app.Environment.IsDevelopment())
            {
                app.MapWhen(x => !x.Request.Path.Value!.StartsWith("/api") && !x.Request.Path.Value.StartsWith("/membership"), builder =>
                {
                    builder.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "ClientApp";
                        // spa.UseAngularCliServer(npmScript: "start");
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    });
                });
            }

            app.Run();
        }
    }
}