using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _supplierRepository.GetAllSuppliersAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierName,MaterialType,Phone,Products")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepository.AddSupplierAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

  
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,SupplierName,MaterialType,Phone,Products")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supplierRepository.UpdateSupplierAsync(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id.Value);
            if (supplier != null)
            {
                await _supplierRepository.DeleteSupplierAsync(supplier.Id);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}