using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
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
            ViewBag.SupplierList = new SelectList(_db.Suppliers.ToList(), "Id", "SupplierName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            ModelState.Remove("Supplier");

            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierList = new SelectList(_db.Suppliers.ToList(), "Id", "SupplierName");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(product);
        }

        //Edit--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = _db.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.SupplierList = new SelectList(_db.Suppliers.ToList(), "Id", "SupplierName");
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ModelState.Remove("Supplier");

            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierList = new SelectList(_db.Suppliers.ToList(), "Id", "SupplierName");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(product);
        }

        //Delete--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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