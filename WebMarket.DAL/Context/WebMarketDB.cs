using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMarketDomain.Entityes;
using WebMarketDomain.Entityes.Identity;
using WebMarketDomain.Entityes.Orders;

namespace WebMarket.DAL.Context
{
    public class WebMarketDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Order> Orders { get; set; }

        public WebMarketDB(DbContextOptions<WebMarketDB> options) : base(options)
        {

        }
    }
}
