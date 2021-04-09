using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using WebMarket.Infrastructure.Mapping;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;
using WebMarketDomain;
using WebMarketDomain.Entityes;

namespace WebMarket.Infrastructure.Services.InCookies
{
    public class InCookiesCartServices : ICartServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IProductData _productData;

        private string _cardName;

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;

                var card_cookies = context.Request.Cookies[_cardName];
                if (card_cookies is null)
                {
                    var card = new Cart();
                    cookies.Append(_cardName, JsonConvert.SerializeObject(card));
                    return card;
                }
                ReplaseCookies(cookies, card_cookies);
                return JsonConvert.DeserializeObject<Cart>(card_cookies);
            }
            set => ReplaseCookies(_httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));

        }

        private void ReplaseCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cardName);
            cookies.Append(_cardName, cookie);
        }

        public InCookiesCartServices(IHttpContextAccessor httpContextAccessor, IProductData productData)
        {
            _httpContextAccessor = httpContextAccessor;
            _productData = productData;

            var user = httpContextAccessor.HttpContext.User;

            var user_name = user.Identity.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            _cardName = $"WebMarket.Card{user_name}";
        }

        public void Add(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
            {
                cart.Items.Add(new CartItem { ProductId = id });
            }
            else
            {
                item.Quantity++;
            }

            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null) return;

            if (item.Quantity > 0)
            {
                item.Quantity--;
            }

            if (item.Quantity <= 0)
            {
                cart.Items.Remove(item);
            }

            Cart = cart;
        }

        public CartViewModel GetViewModel()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToArray()
            });

            var products_views = products.ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = Cart.Items
                    .Where(item => products_views.ContainsKey(item.ProductId))
                    .Select(item => (products_views[item.ProductId], item.Quantity))
            };
        }

        public void Remove(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);
            Cart = cart;
        }
    }
}
