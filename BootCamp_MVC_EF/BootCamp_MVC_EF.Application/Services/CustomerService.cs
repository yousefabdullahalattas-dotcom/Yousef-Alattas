using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _unitOfWork.CustomerRepository
                .GetAllCustomersAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Phone = c.Phone,
                Address = c.Address,
                CustomerType = c.CustomerType
            });
        }

        public async Task<CustomerUpdateDto?> GetCustomerById(int id)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return null;
            }

            return new CustomerUpdateDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                Phone = customer.Phone,
                Address = customer.Address,
                CustomerType = customer.CustomerType
            };
        }


        public async Task AddCustomer(CustomerCreateDto customerDTO)
        {
            var customer = new Customer
            {
                CustomerName = customerDTO.CustomerName,
                Phone = customerDTO.Phone,
                Address = customerDTO.Address,
                CustomerType = customerDTO.CustomerType
            };

            await _unitOfWork.CustomerRepository
                .AddCustomerAsync(customer);
        }

        public async Task UpdateCustomer(CustomerUpdateDto customerDTO)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetCustomerByIdAsync(customerDTO.Id);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            customer.CustomerName = customerDTO.CustomerName;
            customer.Phone = customerDTO.Phone;
            customer.Address = customerDTO.Address;
            customer.CustomerType = customerDTO.CustomerType;

            await _unitOfWork.CustomerRepository
                .UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            await _unitOfWork.CustomerRepository
                .DeleteCustomerAsync(id);
        }
    }
}
