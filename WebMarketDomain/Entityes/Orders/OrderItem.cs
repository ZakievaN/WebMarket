using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMarketDomain.Entityes.Base;

namespace WebMarketDomain.Entityes.Orders
{
    public class OrderItem : Entity
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalItemPrice => Price * Quantity;
    }
}
