using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            IList<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell", Price = 3500 },
                new Product { Id = 2, Name = "iPhone 15", Price = 4200 },
                new Product { Id = 3, Name = "Wireless Mouse USBB", Price = 150 }
            };

            return View(products);
        }
    }
}
