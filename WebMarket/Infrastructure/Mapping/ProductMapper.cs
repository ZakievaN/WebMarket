using System.Collections.Generic;
using System.Linq;
using WebMarket.ViewModels;
using WebMarketDomain.Entityes;

namespace WebMarket.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product)
        { 
            return product is null
            ? null
            : new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price
            };
        }


        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products)
        {
            return products.Select(ToView);
        }
    }
}
