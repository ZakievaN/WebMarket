using System.Collections.Generic;
using WebMarketDomain;
using WebMarketDomain.Entityes;

namespace WebMarket.Infrastructure.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts( ProductFilter filter = null);

        Product GetProductById(int id);

        void Update(Product product);

        bool Delete(int id);
    }
}
