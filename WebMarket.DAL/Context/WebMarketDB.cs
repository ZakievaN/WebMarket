using Microsoft.EntityFrameworkCore;
using WebMarketDomain.Entityes;

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
