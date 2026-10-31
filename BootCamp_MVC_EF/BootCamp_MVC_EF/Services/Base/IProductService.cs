using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductUpdateDto?> GetProductById(int id);

        Task<IEnumerable<SupplierDto>> GetAllSuppliers();

        Task AddProduct(ProductCreateDto productDTO);

        Task UpdateProduct(ProductUpdateDto productDTO);

        Task DeleteProduct(int id);
    }
}
