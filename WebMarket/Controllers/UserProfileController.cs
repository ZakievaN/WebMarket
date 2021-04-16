using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders([FromServices] IOrderService orderService)
        {
            var orders = await orderService.GetUserOrders(User.Identity!.Name);

            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                TotalPrice = order.Items.Sum(item => item.TotalItemPrice)
            }));
        }
    }
}
