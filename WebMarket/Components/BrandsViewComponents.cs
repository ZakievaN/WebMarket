using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Components
{
    public class BrandsViewComponents
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponents(IProductData productData)
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
