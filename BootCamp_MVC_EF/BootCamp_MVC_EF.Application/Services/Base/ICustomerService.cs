using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomers();

        Task<CustomerUpdateDto?> GetCustomerById(int id);

        Task AddCustomer(CustomerCreateDto customerDTO);

        Task UpdateCustomer(CustomerUpdateDto customerDTO);

        Task DeleteCustomer(int id);
    }
}
