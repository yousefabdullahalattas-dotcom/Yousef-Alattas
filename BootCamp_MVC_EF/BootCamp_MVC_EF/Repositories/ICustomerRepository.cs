using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;

namespace BootCamp_MVC_EF.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);

    }
}