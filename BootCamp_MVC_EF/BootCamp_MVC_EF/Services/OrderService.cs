using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepository
                .GetAllOrdersAsync();

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.Customer?.CustomerName ?? string.Empty,
                ProductName = o.Product?.ProductName ?? string.Empty,
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice
            });
        }

        public async Task<OrderUpdateDto?> GetOrderById(int id)
        {
            var order = await _unitOfWork.OrderRepository
                .GetOrderByIdAsync(id);

            if (order == null)
            {
                return null;
            }

            return new OrderUpdateDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };
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

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _unitOfWork.ProductRepository
                .GetAllProductsAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Type = p.Type,
                PowerWatt = p.PowerWatt,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SupplierName = p.Supplier?.SupplierName ?? string.Empty
            });
        }

        public async Task AddOrder(OrderCreateDto orderDTO)
        {
            var order = new Order
            {
                CustomerId = orderDTO.CustomerId,
                ProductId = orderDTO.ProductId,
                Quantity = orderDTO.Quantity,
                OrderDate = orderDTO.OrderDate,
                TotalPrice = orderDTO.TotalPrice
            };

            await _unitOfWork.OrderRepository
                .AddOrderAsync(order);
        }

        public async Task UpdateOrder(OrderUpdateDto orderDTO)
        {
            var order = await _unitOfWork.OrderRepository
                .GetOrderByIdAsync(orderDTO.Id);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            order.CustomerId = orderDTO.CustomerId;
            order.ProductId = orderDTO.ProductId;
            order.Quantity = orderDTO.Quantity;
            order.OrderDate = orderDTO.OrderDate;
            order.TotalPrice = orderDTO.TotalPrice;

            await _unitOfWork.OrderRepository
                .UpdateOrderAsync(order);
        }


        public async Task DeleteOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository
                .GetOrderByIdAsync(id);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            await _unitOfWork.OrderRepository
                .DeleteOrderAsync(id);
        }
    }
}
