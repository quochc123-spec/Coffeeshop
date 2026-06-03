using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using System.Linq;

namespace CoffeeShop.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private CoffeeShopDbContext dbContext;
        private IShoppingCartRepository shoppingCartRepository;
        public OrderRepository(CoffeeShopDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepository = shoppingCartRepository;
        }
        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetShoppingCartItems()?.OfType<ShoppingCartItem>().ToList() ?? new List<ShoppingCartItem>();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                if (item.Product == null)
                    continue;

                var orderDetail = new OrderDetail
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
    }
}
