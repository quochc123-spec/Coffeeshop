using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var trending = productRepository.GetTrendingProducts();
            return View(trending);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Detail()
         { 
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
