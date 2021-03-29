using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebMarket.Infrastructure.Services.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Components
{
    public class SectionsViewComponent : ViewComponent
    {

        private readonly IProductData _ProductData;

        public SectionsViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }

        public IViewComponentResult Invoke ()
        {
            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_view = parent_sections.
                Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                }).ToList();

            foreach(var parent_section in parent_sections_view)
            {
                var child = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in child)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section
                    });

                parent_sections_view.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            return View(parent_sections_view);
        }
    }
}
