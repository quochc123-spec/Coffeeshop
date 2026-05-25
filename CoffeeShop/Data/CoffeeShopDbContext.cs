using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data
{
    public class CoffeeShopDbContext : DbContext
    {
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

    
    //seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "America", Price = 25, Detail = "New product", ImgUrl = "" },
                new Product { Id = 2, Name = "VietNam", Price = 20, Detail = "New product", ImgUrl = "" },
                new Product { Id = 3, Name = "United Kingdom", Price = 15, Detail = "New product", ImgUrl = "" },
                new Product { Id = 4, Name = "India", Price = 25, Detail = "New product", ImgUrl = "" },
                new Product { Id = 5, Name = "Russia", Price = 25, Detail = "New product", ImgUrl = "" },
                new Product { Id = 6, Name = "France", Price = 35, Detail = "New product", ImgUrl = "" }
                );
        }
    }
}
