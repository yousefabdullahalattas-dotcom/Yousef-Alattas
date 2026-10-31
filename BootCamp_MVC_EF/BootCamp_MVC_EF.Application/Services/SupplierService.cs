using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<SupplierUpdateDto?> GetSupplierById(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository
                .GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return null;
            }

            return new SupplierUpdateDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                MaterialType = supplier.MaterialType,
                Phone = supplier.Phone
            };
        }

        public async Task AddSupplier(SupplierCreateDto supplierDTO)
        {
            var supplier = new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                MaterialType = supplierDTO.MaterialType,
                Phone = supplierDTO.Phone
            };

            await _unitOfWork.SupplierRepository
                .AddSupplierAsync(supplier);
        }

        public async Task UpdateSupplier(SupplierUpdateDto supplierDTO)
        {
            var supplier = await _unitOfWork.SupplierRepository
                .GetSupplierByIdAsync(supplierDTO.Id);

            if (supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found");
            }

            supplier.SupplierName = supplierDTO.SupplierName;
            supplier.MaterialType = supplierDTO.MaterialType;
            supplier.Phone = supplierDTO.Phone;

            await _unitOfWork.SupplierRepository
                .UpdateSupplierAsync(supplier);
        }

        public async Task DeleteSupplier(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository
                .GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found");
            }

            await _unitOfWork.SupplierRepository
                .DeleteSupplierAsync(id);
        }
    }
}
