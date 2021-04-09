using Microsoft.AspNetCore.Mvc;
using WebMarket.Infrastructure.Services.Interfaces;

namespace WebMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        public IActionResult Index()
        {
            return View(_cartServices.GetViewModel());
        }

        public IActionResult Add(int id)
        {
            _cartServices.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _cartServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _cartServices.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartServices.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
