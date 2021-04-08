using WebMarketDomain.Entityes.Interfaces;

namespace WebMarketDomain.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
