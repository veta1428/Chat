using AuthServer.Entities;
using AuthServer.ViewModels;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [Route("membership")]
    public class MembershipController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public MembershipController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }

        [HttpGet]
        public IActionResult Register([FromQuery] string? returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, [FromQuery(Name ="returnUrl")] string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, LastName = model.LastName, FirstName = model.FirstName };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(JwtClaimTypes.GivenName, model.FirstName));
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(JwtClaimTypes.FamilyName, model.LastName));

                    await _signInManager.SignInAsync(user, false);
                    if (returnUrl is null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await _interaction.GetAuthorizationContextAsync(returnUrl);
                        await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            ViewData["returnUrl"] = returnUrl;
            return View(model);
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, [FromQuery] string? smth = null)
        {
            await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password");
                }
            }
            return View(model);
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var context = await _interaction.GetLogoutContextAsync(logoutId);


            if (User?.Identity?.IsAuthenticated == true)
            {
                await HttpContext.SignOutAsync();
            }

            return Redirect(context!.PostLogoutRedirectUri);
        }
    }
}
