using System.Collections.Generic;
using System.Linq;

namespace WebMarket.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCount
        {
            get
            {
                return Items?.Sum(item => item.Quantity) ?? 0;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return Items?.Sum(item => item.Product.Price * item.Quantity) ?? 0;
            }
        }
    }
}
