using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMarket.Infrastructure.Mapping;
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
                    .ToView()
            };

            return View(catalogViewModel);
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null)
            {
                return NotFound();
            }

            return View(product.ToView());
        }
    }
}
