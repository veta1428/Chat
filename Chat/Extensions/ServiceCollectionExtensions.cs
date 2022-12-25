using Chat.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

namespace Chat.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string DefaultChallengeScheme = "Challenge";
        public const string ExternalCookieScheme = "External";

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = DefaultChallengeScheme;
            })
            .AddScheme<ChallengeAuthenticationOptions, ChallengeAuthenticationHandler>(DefaultChallengeScheme, options =>
            {
                options.ChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.ChallengeRule = (context) => !context.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase);
                options.PostAuthenticationRedirect = "/membership/externalLoginCallback";
                options.ReturnUrlParameterName = "returnUrl";
            })
            .AddCookie(ExternalCookieScheme, options =>
            {
                options.Cookie.Name = "chat.external.cookie";
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "chat.auth";
                options.Cookie.SameSite = SameSiteMode.Lax;
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = ExternalCookieScheme;
                options.Authority = "http://localhost:5036";
                options.ResponseType = "code";
                options.ClientId = "oidcClient";
                options.ClientSecret = "SuperSecretPassword";
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;

                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("openid");
                //options.Scope.Add("profile");
                //options.Scope.Add("email");
                //options.Scope.Add("role");
                //options.Scope.Add("api1.read");
            });

            services.ConfigureNonBreakingSameSiteCookies();
        }
    }
}