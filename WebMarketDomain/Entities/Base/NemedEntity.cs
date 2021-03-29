using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities.Base
{
    public abstract class NemedEntity : Entity, INameEntity
    {
        public string Name { get; set; }
    }
}
