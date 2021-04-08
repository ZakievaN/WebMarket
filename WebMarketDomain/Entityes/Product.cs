﻿using System.ComponentModel.DataAnnotations.Schema;
using WebMarketDomain.Entityes;
using WebMarketDomain.Entityes.Base;
using WebMarketDomain.Entityes.Interfaces;

namespace WebMarketDomain.Entityes
{
    public class Product : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }

    

}
