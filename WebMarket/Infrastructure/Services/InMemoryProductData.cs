using System.Collections.Generic;
using WebMarket.Data;
using WebMarketDomain.Entities;

namespace WebMarket.Infrastructure.Services
{
    public class InMemoryProductData : Interfaces.IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
    }
}
