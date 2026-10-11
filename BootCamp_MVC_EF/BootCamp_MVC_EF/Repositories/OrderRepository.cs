using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }


        public Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            return _context.SaveChangesAsync();
        }

        public Task DeleteOrderAsync(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            IEnumerable<OrderDto> orders = await _context.Orders.Select(o => new OrderDto
            {
                //Mapping the properties of Order to OrderDto
                Id = o.Id,
                CustomerName = o.Customer != null ? o.Customer.CustomerName : null,
                ProductName = o.Product != null ? o.Product.ProductName : null,
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice
            }).ToListAsync();
            return orders;
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;


        }
        public Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            return _context.SaveChangesAsync();

        }
    }
}