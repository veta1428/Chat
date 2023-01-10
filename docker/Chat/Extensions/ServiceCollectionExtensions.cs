using Chat.Auth;
using Chat.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Chat.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string DefaultChallengeScheme = "Challenge";
        public const string ExternalCookieScheme = "External";

        public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var authorityOptions = configuration.GetSection("AuthorityOptions").Get<AuthorityOptions>();

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
                options.Authority = authorityOptions.Authority;
                options.ResponseType = "code id_token";
                options.ClientId = "oidcClient";
                options.ClientSecret = "SuperSecretPassword";
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;

                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("openid");

                options.Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProviderForSignOut = async (context) =>
                    {
                        var tokenName = "id_token";
                        context.ProtocolMessage.IdTokenHint =
                            await context.HttpContext.GetTokenAsync(tokenName) ??
                            await context.HttpContext.GetTokenAsync("External", tokenName);
                    }
                };

                options.Events.OnSignedOutCallbackRedirect += context =>
                {
                    context.Response.Redirect(context.Options.SignedOutRedirectUri);
                    context.HandleResponse();

                    return Task.CompletedTask;
                };
            });

            services.ConfigureNonBreakingSameSiteCookies();
        }
    }
}