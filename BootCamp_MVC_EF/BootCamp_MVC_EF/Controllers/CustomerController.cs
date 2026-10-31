using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

   
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomers();

            return View(customers);
        }


        public IActionResult Create()
        {
            return View(new CustomerCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateDto customerDTO)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomer(customerDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(customerDTO);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerUpdateDto customerDTO)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer =
                    await _customerService.GetCustomerById(customerDTO.Id);

                if (existingCustomer == null)
                {
                    return NotFound();
                }

                await _customerService.UpdateCustomer(customerDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(customerDTO);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
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

            var customer = await _customerService.GetCustomerById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteCustomer(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}
