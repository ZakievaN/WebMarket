using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.ViewModels;
using WebMarketDomain.Entityes.Orders;

namespace WebMarket.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string userName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel cart, OrderViewModel orderView);
    }
}
