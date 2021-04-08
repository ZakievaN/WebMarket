using System.ComponentModel.DataAnnotations;
using WebMarketDomain.Entityes.Interfaces;

namespace WebMarketDomain.Entityes.Base
{
    public abstract class NamedEntity : Entity, INameEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
