using System.Collections.Generic;
using System.Linq;

namespace WebMarketDomain.Entityes
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount()
        {
            return Items?.Sum(Items => Items.Quantity) ?? 0;
        }
    }
}
