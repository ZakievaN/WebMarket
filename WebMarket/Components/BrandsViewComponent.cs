using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }

        public IViewComponentResult Invoke()
        {
            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            return
            _ProductData.GetBrands()
                .OrderBy(b => b.Order)
                .Select(b => new BrandViewModel
                {
                    Id = b.Id,
                    Name = b.Name
                });
        }
    }
}
