using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.DAL.Context;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;
using WebMarketDomain.Entityes.Identity;
using WebMarketDomain.Entityes.Orders;

namespace WebMarket.Infrastructure.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebMarketDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebMarketDB db, UserManager<User> userManager) 
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Order> CreateOrder(string userName, CartViewModel cart, OrderViewModel orderView)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user is null)
            {
                throw new InvalidOperationException($"Пользователь с именем {userName} в БД не найден");
            }

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = orderView.Name,
                Address = orderView.Address,
                Phone = orderView.Phone,
                User = user
            };

            var product_ids = cart.Items.Select(item => item.Product.Id).ToArray();

            var cart_products = await _db.Products
                .Where(p => product_ids.Contains(p.Id))
                .ToArrayAsync();

            order.Items = cart.Items.Join(
                cart_products,
                cart_item => cart_item.Product.Id,
                product => product.Id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price,
                    Quantity = cart_item.Quantity
                }).ToArray();

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return
            await _db.Orders
                .Include(order => order.User)
                .Include(order => order.Items)
                .FirstOrDefaultAsync(Order => Order.Id == id);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string userName)
        {
            return
            await _db.Orders
                .Include(order => order.User)
                .Include(order => order.Items)
                .Where(Order => Order.User.UserName == userName)
                .ToArrayAsync();
        }
    }
}
