using System;
using System.Collections.Generic;
using WebMarketDomain.Entityes.Base;
using WebMarketDomain.Entityes.Identity;

namespace WebMarketDomain.Entityes.Orders
{
    public class Order : NamedEntity
    {
        public User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    }


}
