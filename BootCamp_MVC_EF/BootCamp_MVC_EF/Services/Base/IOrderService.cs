using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();

        Task<OrderUpdateDto?> GetOrderById(int id);

        Task<IEnumerable<CustomerDto>> GetAllCustomers();

        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task AddOrder(OrderCreateDto orderDTO);

        Task UpdateOrder(OrderUpdateDto orderDTO);

        Task DeleteOrder(int id);
    }
}
