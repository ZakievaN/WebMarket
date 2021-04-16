using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebMarketDomain.Entityes;

namespace WebMarket.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

    }

    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

    }
}
