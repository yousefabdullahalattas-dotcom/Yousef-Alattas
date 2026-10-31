using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliers();

        Task<SupplierUpdateDto?> GetSupplierById(int id);

        Task AddSupplier(SupplierCreateDto supplierDTO);

        Task UpdateSupplier(SupplierUpdateDto supplierDTO);

        Task DeleteSupplier(int id);
    }
}
