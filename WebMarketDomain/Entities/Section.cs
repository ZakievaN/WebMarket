using WebMarketDomain.Entities.Base;
using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities
{
    public class Section : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
