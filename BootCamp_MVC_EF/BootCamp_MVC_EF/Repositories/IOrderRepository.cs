using BootCamp_MVC_EF.Models;

namespace BootCamp_MVC_EF.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task AddOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task DeleteOrderAsync(int id);
    }
}
