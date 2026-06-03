namespace CoffeeShop.Models.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<ShoppingCartItem> GetAllShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        IEnumerable<object> GetShoppingCartItems();

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
