using Chat.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

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

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllers();

            app.MapFallbackToFile("index.html");

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";
                    // spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                });
            });

            app.Run();
        }
    }
}