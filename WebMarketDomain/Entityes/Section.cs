using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebMarketDomain.Entityes.Base;
using WebMarketDomain.Entityes.Interfaces;

namespace WebMarketDomain.Entityes
{
    public class Section : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
