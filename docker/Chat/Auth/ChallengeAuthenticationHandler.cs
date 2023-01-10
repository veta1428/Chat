using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Chat.Auth
{
    public class ChallengeAuthenticationHandler : AuthenticationHandler<ChallengeAuthenticationOptions>
    {
        public ChallengeAuthenticationHandler(IOptionsMonitor<ChallengeAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            if (Options.ChallengeRule == null || Options.ChallengeRule(Context))
            {
                properties.RedirectUri = Options.PostAuthenticationRedirect;
                properties.Items.Add(Options.ReturnUrlParameterName, UriHelper.GetEncodedPathAndQuery(Request));
                await Context.ChallengeAsync(Options.ChallengeScheme, properties);
            }
            else
            {
                await Context.ForbidAsync();
            }
        }
    }
}
