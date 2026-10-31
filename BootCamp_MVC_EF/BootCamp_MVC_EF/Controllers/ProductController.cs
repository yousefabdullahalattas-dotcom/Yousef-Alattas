using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSuppliersAsync();

            return View(new ProductCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateDto productDTO, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    productDTO.ImageUrl = UploadImage(image);
                }

                await _productService.AddProduct(productDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadSuppliersAsync();

            return View(productDTO);
        }

        private string UploadImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "images",
      "products"
    );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/products/" + fileName;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            await LoadSuppliersAsync();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductUpdateDto productDTO)
        {
            if (ModelState.IsValid)
            {
                var existingProduct =
                    await _productService.GetProductById(productDTO.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                await _productService.UpdateProduct(productDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadSuppliersAsync();

            return View(productDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id.Value);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSuppliersAsync()
        {
            var suppliers = await _productService.GetAllSuppliers();

            ViewBag.Suppliers = new SelectList(
                suppliers,
                "Id",
                "SupplierName"
            );
        }
    }
}
