using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebMarketDomain.Entityes.Identity;
using WebMarket.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebMarket.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
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

            _logger.LogInformation($"Регистрация пользователя {user.UserName}");

            var registration_result = await _userManager.CreateAsync(user, model.Password);
            if (registration_result.Succeeded)
            {
                _logger.LogInformation($"Пользователь {user.UserName} успешно зарегистрирован");

                await _userManager.AddToRoleAsync(user, Role.Users);

                _logger.LogInformation($"Пользователь {user.UserName} наделен ролью {Role.Users}");

                await _signInManager.SignInAsync(user, false);

                _logger.LogInformation($"Пользователь {user.UserName} вошел в систему");

                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning($"Ошибка при регистрации пользователя {user.UserName} с ошибкой " +
                $"{string.Join(", ", registration_result.Errors.Select(e => e.Description))}");

            foreach (var error in registration_result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
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
                _logger.LogInformation($"Пользователь {model.UserName} вошел в систему");

                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            _logger.LogWarning($"Ошибка авторизации пользователя {model.UserName} с неверным логином/паролем");


            ModelState.AddModelError("", "Неверное имя пользователя или пароль!");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity.Name;

            await _signInManager.SignOutAsync();

            _logger.LogInformation($"Пользователь {user_name} вышел из системы");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
