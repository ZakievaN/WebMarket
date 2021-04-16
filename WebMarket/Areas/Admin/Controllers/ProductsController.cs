using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;
using WebMarketDomain.Entityes;
using WebMarketDomain.Entityes.Identity;

namespace WebMarket.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_productData.GetProducts());
        }

        [HttpGet]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int id)
        {
            var product = _productData.GetProductById(id);
            
            if (product == null)
            {
                return NotFound();
            }

            SelectList brands = new SelectList(_productData.GetBrands(), "Id", "Name", product.BrandId);

            SelectList sections = new SelectList(_productData.GetSections(), "Id", "Name", product.SectionId);

            ViewBag.Brands = brands;
            ViewBag.Sections = sections;

            return View(new ProductViewModel
            {
                Id = product.Id,
                BrandId = product.BrandId,
                SectionId = product.SectionId,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Name = product.Name
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                BrandId = model.BrandId,
                SectionId = model.SectionId
            };

            _productData.Update(product);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                BrandId = product.BrandId,
                SectionId = product.SectionId
            });
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _productData.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
