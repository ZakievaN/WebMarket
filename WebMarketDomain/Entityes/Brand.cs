using System.Collections.Generic;
using WebMarketDomain.Entityes;
using WebMarketDomain.Entityes.Base;
using WebMarketDomain.Entityes.Interfaces;

namespace WebMarketDomain.Entityes
{
    public class Brand : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
