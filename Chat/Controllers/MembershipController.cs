using Chat.Auth;
using Chat.EF;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chat.Controllers
{
    [Route("[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly ChatContext _context;
        public MembershipController(ChatContext context)
        {
            _context = context;
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

            var user = await _context.Users
                .Where(u => u.IdentityId == identityId)
                .SingleAsync(cancellationToken);

            var sid = external!.FindFirst(JwtRegisteredClaimNames.Sid);

            var email = external!.FindFirst(JwtRegisteredClaimNames.Name).Value;

            var identity = new Identity()
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };

            identity.AddClaim(sid!);

            
            await HttpContext.SignOutAsync("External");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), result.Properties);

            var returnUrl = result.Properties!.Items["returnUrl"] ?? "~/";

            return LocalRedirect(returnUrl ?? "~/");
        }
    }
}
