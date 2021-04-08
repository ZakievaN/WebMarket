using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebMarketDomain.Entityes.Identity;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User> _userManager;
        public SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName
            };

            var registration_result = await _userManager.CreateAsync(user, model.Password);
            if (registration_result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel {ReturnUrl = ReturnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var login_result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.IsRemember,

#if DEBUG
                false
#else
                true
#endif
                );

            if (login_result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль!");
            return View(model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
