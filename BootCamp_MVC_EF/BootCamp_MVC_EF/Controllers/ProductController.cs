using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
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


            IEnumerable<ProductDto> products = _db.Products.Select(p => new ProductDto
            {

                Id = p.Id,
                ProductName = p.ProductName,
                Type = p.Type,
                PowerWatt = p.PowerWatt,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SupplierName = p.Supplier != null ? p.Supplier.SupplierName : null
            }).ToList();

            return View(products);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            loadSuppliers();
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductUpdateDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Type = product.Type,
                PowerWatt = product.PowerWatt,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                SupplierId = product.SupplierId
            };

            return View(productDTO);
        }

        [HttpPost]
        public ActionResult Edit(ProductUpdateDto productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.Find(productDTO.Id);
                if (product == null)
                {
                    return NotFound();
                }

                product.ProductName = productDTO.ProductName;
                product.Type = productDTO.Type;
                product.PowerWatt = productDTO.PowerWatt;
                product.Price = productDTO.Price;
                product.StockQuantity = productDTO.StockQuantity;
                product.SupplierId = productDTO.SupplierId;


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadSuppliers();
            return View(productDTO);

        }


        private void loadSuppliers()
        {
            var suppliers = _db.Suppliers.ToList();
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "SupplierName");
        }


        public ActionResult Create()
        {
            loadSuppliers();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCreateDto productDTO)
        {
            if (ModelState.IsValid)
            {

                var product = new Product
                {
                    ProductName = productDTO.ProductName,
                    Type = productDTO.Type,
                    PowerWatt = productDTO.PowerWatt,
                    Price = productDTO.Price,
                    StockQuantity = productDTO.StockQuantity,
                    SupplierId = productDTO.SupplierId
                };

                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            loadSuppliers();
            return View(productDTO);

        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductUpdateDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Type = product.Type,
                PowerWatt = product.PowerWatt,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                SupplierId = product.SupplierId
            };

            return View(productDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}