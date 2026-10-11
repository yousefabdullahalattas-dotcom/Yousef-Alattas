using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _customerRepository.GetAllCustomersAsync());
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDTO = new CustomerUpdateDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                Address = customer.Address,
                CustomerType = customer.CustomerType
            };

            return View(customerDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerUpdateDto customerDTO)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(customerDTO.Id);
                if (customer == null)
                {
                    return NotFound();
                }

                customer.CustomerName = customerDTO.CustomerName;
                customer.Phone = customerDTO.Phone;
                customer.Address = customerDTO.Address;
                customer.CustomerType = customerDTO.CustomerType;

                await _customerRepository.UpdateCustomerAsync(customer);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(customerDTO);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDto customerDTO)
        {
            if (ModelState.IsValid)
            {

                var customer = new Customer
                {
                    CustomerName = customerDTO.CustomerName,
                    Phone = customerDTO.Phone,
                    Address = customerDTO.Address,
                    CustomerType = customerDTO.CustomerType
                };

                await _customerRepository.AddCustomerAsync(customer);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(customerDTO);

        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDTO = new CustomerUpdateDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                Address = customer.Address,
                CustomerType = customer.CustomerType
            };

            return View(customerDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomerAsync(customer.Id);

            return RedirectToAction("Index");
        }

    }
}