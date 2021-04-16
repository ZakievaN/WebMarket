using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;

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
            var model = new CartOrderViewModel {
                Cart = _cartServices.GetViewModel(),
                };
            return View(model);
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

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartServices.GetViewModel(),
                    Order = orderModel
                });

            var order = await orderService.CreateOrder(
                User.Identity!.Name,
                _cartServices.GetViewModel(),
                orderModel
                );

            _cartServices.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
