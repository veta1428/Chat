using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat.Controllers
{
    [Route("[controller]")]
    public class MembershipController : ControllerBase
    {
        public MembershipController()
        {
        }
        [Authorize]
        [Route("login")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Login(string? returnUrl)
        {
            return LocalRedirect(string.IsNullOrWhiteSpace(returnUrl) ? "~/" : returnUrl);
        }
        
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task Logout(string? returnUrl)
        {
            throw new NotImplementedException("Logout not implemented");
            //await _signInManager.SignOutAsync(returnUrl);
        }

        [Route("externalLoginCallback")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ExternalLoginCallback(CancellationToken cancellationToken)
        {
            //var signInResult = await _signInManager.SignInAsync(cancellationToken);

            var result = await HttpContext.AuthenticateAsync("External");

            if (!result.Succeeded)
                throw new Exception("Login external not succeded");

            var external = result.Principal!.Identity as ClaimsIdentity;

            var sub = external?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!int.TryParse(sub, out var identityId))
            {
                throw new Exception("...");
            }

            var user = identityId;

            throw new NotImplementedException("Login not implemented yet");

            return new EmptyResult();
        }
    }
}
