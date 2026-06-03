using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace coffeeshop.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CoffeeShopDbContext dbContext;
        public ShoppingCartRepository(CoffeeShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // initialize to avoid possible null dereference when using this property
        // non-nullable to match IShoppingCartRepository; use target-typed new() per IDE0028
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();

        public string? ShoppingCartId { set; get; }
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {

            ISession? session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            CoffeeShopDbContext context = services.GetService<CoffeeShopDbContext>() ??
            throw new Exception("Error initializing coffeeshopdbcontext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s =>
                // guard s.Product against null before accessing its Id
                s.Product != null && s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            dbContext.SaveChanges();
        }
        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId ==
            ShoppingCartId);
            dbContext.ShoppingCartItems.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }
        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            // assign directly because ShoppingCartItems is non-nullable
            ShoppingCartItems = dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();
            return ShoppingCartItems;
        }

        public IEnumerable<object> GetShoppingCartItems()
        {
            throw new NotImplementedException();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCartItems.Where(s => s.ShoppingCartId ==
            ShoppingCartId)
            // guard s.Product against null; treat null product as zero cost
            .Select(s => (s.Product != null ? s.Product.Price * s.Qty : 0m)).Sum();
            return totalCost;
        }
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s =>
                // guard s.Product against null before accessing its Id
                s.Product != null && s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            var quantity = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;

                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }
    }
}