using BootCamp_MVC_EF.Application.Dtos;
using BootCamp_MVC_EF.Application.Services.Base;
using BootCamp_MVC_EF.Domain.Models;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Infrastructure.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                ImageUrl = p.ImageUrl,
                SupplierName = p.Supplier?.SupplierName ?? string.Empty
            });
        }

        // GET BY ID
        public async Task<ProductUpdateDto?> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductUpdateDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Type = product.Type,
                PowerWatt = product.PowerWatt,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                SupplierId = product.SupplierId
            };
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliers()
        {
            var suppliers = await _unitOfWork.SupplierRepository
                .GetAllSuppliersAsync();

            return suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                SupplierName = s.SupplierName,
                MaterialType = s.MaterialType,
                Phone = s.Phone
            });
        }

        public async Task AddProduct(ProductCreateDto productDTO)
        {
            var product = new Product
            {
                ProductName = productDTO.ProductName,
                Type = productDTO.Type,
                PowerWatt = productDTO.PowerWatt,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
                ImageUrl = productDTO.ImageUrl,
                SupplierId = productDTO.SupplierId
            };

            await _unitOfWork.ProductRepository
                .AddProductAsync(product);
        }

        public async Task UpdateProduct(ProductUpdateDto productDTO)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(productDTO.Id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.ProductName = productDTO.ProductName;
            product.Type = productDTO.Type;
            product.PowerWatt = productDTO.PowerWatt;
            product.Price = productDTO.Price;
            product.StockQuantity = productDTO.StockQuantity;
            product.SupplierId = productDTO.SupplierId;

            await _unitOfWork.ProductRepository
                .UpdateProductAsync(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository
                .GetProductByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            await _unitOfWork.ProductRepository
                .DeleteProductAsync(id);
        }
    }
}
