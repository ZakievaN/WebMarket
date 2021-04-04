using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarketDomain.Entities.Base;
using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities
{
    public class Brand : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
    }
}
