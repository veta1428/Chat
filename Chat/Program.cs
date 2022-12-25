using Chat.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Chat.Extensions;

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
            builder.Services.AddControllersWithViews();
            builder.Services.ConfigureAuthorization();

            var app = builder.Build();

            //app.UseCors(
            //    options => options.AllowAnyOrigin()
            //);

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

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api") && !x.Request.Path.Value.StartsWith("/membership"), builder =>
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