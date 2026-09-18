using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _db;
        public SuppliersController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Supplier> suppliers = _db.Suppliers.ToList();
            return View(suppliers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Suppliers.Add(supplier);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(supplier);
        }

        //Edit---------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var supplier = _db.Suppliers.Find(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Suppliers.Update(supplier);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(supplier);
        }

        //Delete------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var supplier = _db.Suppliers.Find(Id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost]
        public ActionResult Delete(Supplier supplier)
        {
            _db.Suppliers.Remove(supplier);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

