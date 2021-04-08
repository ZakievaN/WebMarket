using Microsoft.AspNetCore.Mvc;
using WebMarket.ViewModels;


namespace WebMarket.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
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
