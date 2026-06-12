using CoffeeShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;    
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data
{
    public class CoffeeShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public object? Order { get; internal set; }

        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "America", Price = 25, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:Espresso.jpg" },
                new Product { Id = 2, Name = "VietNam", Price = 20, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:White_coffee_in_Vietnam.jpg" },
                new Product { Id = 3, Name = "United Kingdom", Price = 15, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:Kafae_boran_Chiang_Mai.jpg" },
                new Product { Id = 4, Name = "India", Price = 25, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:Latte_macchiato_with_coffee_beans.jpg" },
                new Product { Id = 5, Name = "Russia", Price = 25, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:Mug_of_coffee_at_Black_Lion_High_Roding,_Essex,_England.jpg" },
                new Product { Id = 6, Name = "France", Price = 35, Detail = "New product", ImgUrl = "https://vi.wikipedia.org/wiki/T%E1%BA%ADp_tin:Latte.jpg" }
                );
        }
    }
}
