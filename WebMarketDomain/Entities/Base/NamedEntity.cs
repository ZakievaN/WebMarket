using System.ComponentModel.DataAnnotations;
using WebMarketDomain.Entities.Interfaces;

namespace WebMarketDomain.Entities.Base
{
    public abstract class NamedEntity : Entity, INameEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
