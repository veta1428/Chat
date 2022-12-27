using AuthServer.Entities;
using AuthServer.ViewModels;
using Duende.IdentityServer.Services;
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, LastName = model.LastName, FirstName = model.FirstName };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // ToDo: return smth adequate
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
            var h = HttpContext;

            await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
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
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // удаляем аутентификационные куки
            //await _signInManager.SignOutAsync();
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            var showSignoutPrompt = context?.ShowSignoutPrompt != false;

            if (User?.Identity?.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
            }

            //context.PostLogoutRedirectUri = "http://localhost:5089/signout-callback-oidc";
            return Redirect(context!.PostLogoutRedirectUri);
            //return Ok(new
            //{
            //    showSignoutPrompt,
            //    ClientName = string.IsNullOrEmpty(context?.ClientName) ? context?.ClientId : context?.ClientName,
            //    context?.PostLogoutRedirectUri,
            //    context?.SignOutIFrameUrl,
            //    logoutId
            //});
        }
    }
}
