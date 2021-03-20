using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}