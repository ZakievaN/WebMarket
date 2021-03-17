using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.Controllers
{ 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("First controller action");
        }

        public IActionResult SecondAction(string id)
        {
            return Content($"Action with value id: {id}");
        }
    }
}
