using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            return _context.SaveChangesAsync();
        }

        public Task DeleteCustomerAsync(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            IEnumerable<CustomerDto> customers = await _context.Customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Phone = c.Phone,
                Address = c.Address,
                CustomerType = c.CustomerType
            }).ToListAsync();
            return customers;
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer;


        }
        public Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return _context.SaveChangesAsync();

        }
    }
}