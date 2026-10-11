using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChangesAsync();
        }

        public Task DeleteProductAsync(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            IEnumerable<ProductDto> products = await _context.Products.Select(p => new ProductDto
            {
                //Mapping the properties of Product to ProductDto
                Id = p.Id,
                ProductName = p.ProductName,
                Type = p.Type,
                PowerWatt = p.PowerWatt,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SupplierName = p.Supplier != null ? p.Supplier.SupplierName : null
            }).ToListAsync();
            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;


        }
        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return _context.SaveChangesAsync();

        }
    }
}