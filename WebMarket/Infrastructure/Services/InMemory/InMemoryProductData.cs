using System.Collections.Generic;
using System.Linq;
using WebMarket.Data;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarketDomain;
using WebMarketDomain.Entityes;

namespace WebMarket.Infrastructure.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter = null)
        {
            var query = TestData.Products;

            if (filter?.SectionId is { } section_id)
            {
                query = query.Where(product => product.SectionId == section_id);
            }

            if (filter?.BrandId is { } brand_id)
            {
                query = query.Where(product => product.BrandId == brand_id);
            }

            return query;
        }

        public Product GetProductById(int id)
        {
            return TestData.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
