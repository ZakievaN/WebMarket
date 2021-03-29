using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
