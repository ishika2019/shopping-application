using Microsoft.EntityFrameworkCore;
using project.Entities;
using System.Security.Cryptography;

namespace project.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Productbrand { get; set; }
        public DbSet<ProductType> Producttype { get; set; }

        public DbSet<CustomerBasket> CustomerBasket { get; set; }   
        public DbSet<BasketItem> BasketItems { get; set; }

       


    }
}
