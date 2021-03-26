using Microsoft.AspNetCore.Mvc;
using System;
using WebMarket.Infrastructure.Conventions;
using WebMarket.Infrastructure.Filters;

namespace WebMarket.Controllers
{
    [ActionDescription("Главный контроллер")]
    public class HomeController : Controller
    {
        [ActionDescription("Главное действие")]
        [AddHeader("Test", "Header value")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Throw() => throw new ApplicationException("Test Error");

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
    }
}