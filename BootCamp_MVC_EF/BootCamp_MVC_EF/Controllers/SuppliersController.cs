using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            var supplierDTO = new SupplierUpdateDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                MaterialType = supplier.MaterialType,
                Phone = supplier.Phone
            };

            return View(supplierDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierUpdateDto supplierDTO)
        {
            if (ModelState.IsValid)
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierDTO.Id);
                if (supplier == null)
                {
                    return NotFound();
                }

                supplier.SupplierName = supplierDTO.SupplierName;
                supplier.MaterialType = supplierDTO.MaterialType;
                supplier.Phone = supplierDTO.Phone;

                await _supplierRepository.UpdateSupplierAsync(supplier);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(supplierDTO);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateDto supplierDTO)
        {
            if (ModelState.IsValid)
            {

                var supplier = new Supplier
                {
                    SupplierName = supplierDTO.SupplierName,
                    MaterialType = supplierDTO.MaterialType,
                    Phone = supplierDTO.Phone
                };

                await _supplierRepository.AddSupplierAsync(supplier);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(supplierDTO);

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

            var supplierDTO = new SupplierUpdateDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                MaterialType = supplier.MaterialType,
                Phone = supplier.Phone
            };

            return View(supplierDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
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

            await _supplierRepository.DeleteSupplierAsync(supplier.Id);

            return RedirectToAction("Index");
        }

    }
}