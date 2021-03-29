using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarketDomain.Entities;

namespace WebMarket.Infrastructure.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();
    }
}
