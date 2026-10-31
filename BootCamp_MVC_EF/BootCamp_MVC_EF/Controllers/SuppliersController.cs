using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllSuppliers();

            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View(new SupplierCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateDto supplierDTO)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.AddSupplier(supplierDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(supplierDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierById(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SupplierUpdateDto supplierDTO)
        {
            if (ModelState.IsValid)
            {
                var existingSupplier =
                    await _supplierService.GetSupplierById(supplierDTO.Id);

                if (existingSupplier == null)
                {
                    return NotFound();
                }

                await _supplierService.UpdateSupplier(supplierDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(supplierDTO);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierById(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
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

            var supplier = await _supplierService.GetSupplierById(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            await _supplierService.DeleteSupplier(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}
