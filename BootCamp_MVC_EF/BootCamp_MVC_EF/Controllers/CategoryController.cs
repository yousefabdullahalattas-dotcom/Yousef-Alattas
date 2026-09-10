using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" }
            };

            return View(categories);
        }

    }
}

