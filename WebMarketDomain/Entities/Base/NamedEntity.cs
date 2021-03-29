using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities.Base
{
    public abstract class NamedEntity : Entity, INameEntity
    {
        public string Name { get; set; }
    }
}
