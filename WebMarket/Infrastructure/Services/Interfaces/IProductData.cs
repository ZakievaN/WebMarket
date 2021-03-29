using System.Collections.Generic;
using WebMarketDomain;
using WebMarketDomain.Entities;

namespace WebMarket.Infrastructure.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts( ProductFilter filter = null);
    }
}
