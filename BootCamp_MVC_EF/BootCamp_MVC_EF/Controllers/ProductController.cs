using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(product);
        }


        //Edit-----------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = _db.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(product);
        }

        //Delete-------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var product = _db.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
