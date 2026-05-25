namespace CoffeeShop.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> Shop();
        IEnumerable<Product> GetTrendingProducts();
        IEnumerable<Product>? GetProductDetail(int id);
    }
}
