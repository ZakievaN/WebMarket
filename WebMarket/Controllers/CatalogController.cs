using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;
using WebMarketDomain;

namespace WebMarket.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData productData)
        {
            _ProductData = productData;
        }

        public IActionResult Index(int? brandId, int? sectionId)
        {
            var filter = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId
            };

            var products = _ProductData.GetProducts(filter);

            var catalogViewModel = new CatalogViewModel
            {
                SectionId = sectionId,
                BrandId = brandId,
                Products = products
                    .OrderBy(p => p.Order)
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
            };

            return View(catalogViewModel);
        }
    }
}
