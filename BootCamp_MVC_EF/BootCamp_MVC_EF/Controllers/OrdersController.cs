using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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


            IEnumerable<OrderDto> orders = _db.Orders.Select(o => new OrderDto
            {

                Id = o.Id,
                CustomerName = o.Customer != null ? o.Customer.CustomerName : null,
                ProductName = o.Product != null ? o.Product.ProductName : null,
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice
            }).ToList();

            return View(orders);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            loadCustomers();
            loadProducts();
            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }


            var orderDTO = new OrderUpdateDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };

            return View(orderDTO);
        }

        [HttpPost]
        public ActionResult Edit(OrderUpdateDto orderDTO)
        {
            if (ModelState.IsValid)
            {
                var order = _db.Orders.Find(orderDTO.Id);
                if (order == null)
                {
                    return NotFound();
                }


                order.CustomerId = orderDTO.CustomerId;
                order.ProductId = orderDTO.ProductId;
                order.Quantity = orderDTO.Quantity;
                order.OrderDate = orderDTO.OrderDate;
                order.TotalPrice = orderDTO.TotalPrice;


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadCustomers();
            loadProducts();
            return View(orderDTO);

        }


        private void loadCustomers()
        {
            var customers = _db.Customers.ToList();
            ViewBag.Customers = new SelectList(customers, "Id", "CustomerName");
        }


        private void loadProducts()
        {
            var products = _db.Products.ToList();
            ViewBag.Products = new SelectList(products, "Id", "ProductName");
        }


        public ActionResult Create()
        {
            loadCustomers();
            loadProducts();

            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderCreateDto orderDTO)
        {
            if (ModelState.IsValid)
            {


                var order = new Order
                {
                    CustomerId = orderDTO.CustomerId,
                    ProductId = orderDTO.ProductId,
                    Quantity = orderDTO.Quantity,
                    OrderDate = orderDTO.OrderDate,
                    TotalPrice = orderDTO.TotalPrice
                };

                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadCustomers();
            loadProducts();
            return View(orderDTO);

        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }


            var orderDTO = new OrderUpdateDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };

            return View(orderDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}