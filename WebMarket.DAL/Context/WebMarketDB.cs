using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarketDomain.Entities;

namespace WebMarket.DAL.Context
{
    public class WebMarketDB : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public WebMarketDB(DbContextOptions<WebMarketDB> options) : base(options)
        {

        }
    }
}
