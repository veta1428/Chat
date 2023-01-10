using Microsoft.AspNetCore.Authentication;

namespace Chat.Auth
{
    public class ChallengeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string ChallengeScheme { get; set; } = null!;

        public string PostAuthenticationRedirect { get; set; } = null!;

        public string ReturnUrlParameterName { get; set; } = null!;

        public Func<HttpContext, bool>? ChallengeRule { get; set; }
    }
}
