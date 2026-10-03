using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Order> orders = _db.Orders
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .ToList();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(_db.Customers.ToList(), "Id", "CustomerName");
            ViewBag.ProductList = new SelectList(_db.Products.ToList(), "Id", "ProductName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            ModelState.Remove("Customer");
            ModelState.Remove("Product");

            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerList = new SelectList(_db.Customers.ToList(), "Id", "CustomerName");
            ViewBag.ProductList = new SelectList(_db.Products.ToList(), "Id", "ProductName");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(order);
        }

        //Edit---------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var order = _db.Orders.Find(Id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.CustomerList = new SelectList(_db.Customers.ToList(), "Id", "CustomerName");
            ViewBag.ProductList = new SelectList(_db.Products.ToList(), "Id", "ProductName");
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            ModelState.Remove("Customer");
            ModelState.Remove("Product");

            if (ModelState.IsValid)
            {
                _db.Orders.Update(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerList = new SelectList(_db.Customers.ToList(), "Id", "CustomerName");
            ViewBag.ProductList = new SelectList(_db.Products.ToList(), "Id", "ProductName");
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(order);
        }

        //Delete--------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var order = _db.Orders
                .Include(x => x.Customer)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == Id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(Order order)
        {
            var existingOrder = _db.Orders.Find(order.Id);
            _db.Orders.Remove(existingOrder);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}