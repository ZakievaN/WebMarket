using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebMarket.DAL.Context;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarketDomain;
using WebMarketDomain.Entityes;

namespace WebMarket.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebMarketDB _db;

        public SqlProductData(WebMarketDB db)
        {
            _db = db;
        }

        public Product GetProductById(int id)
        {
            return
            _db.Products
                .Include(p => p.Section)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
        }

        IEnumerable<Brand> IProductData.GetBrands()
        {
            return _db.Brands.Include(s => s.Products);
        }

        IEnumerable<Product> IProductData.GetProducts(ProductFilter filter)
        {
            IQueryable<Product> query = _db.Products;

            if (filter?.Ids.Length > 0)
            {
                query = query.Where(product => filter.Ids.Contains(product.Id));
            }
            else
            {
                if (filter?.SectionId is { } section_id)
                {
                    query = query.Where(product => product.SectionId == section_id);
                }

                if (filter?.BrandId is { } brand_id)
                {
                    query = query.Where(product => product.BrandId == brand_id);
                }
            }

            return query;
        }

        IEnumerable<Section> IProductData.GetSections()
        {
            return _db.Sections.Include(s => s.Products);
        }
    }
}
