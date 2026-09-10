using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Ahmed", Email = "you@sae", Phone = "0508877" },
                new Customer { Id = 2, Name = "Yousef", Email = "Bob@gt", Phone = "5755" },
                new Customer { Id = 3, Name = "Mohammed", Email = "Charlie@43", Phone = "86565" }
            };
            return View(customers);
        }
    }
}
