using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_MVC_EF.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {

        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }


        public Task AddSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            return _context.SaveChangesAsync();
        }

        public Task DeleteSupplierAsync(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }
        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            IEnumerable<SupplierDto> suppliers = await _context.Suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                SupplierName = s.SupplierName,
                MaterialType = s.MaterialType,
                Phone = s.Phone
            }).ToListAsync();
            return suppliers;
        }
        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            return supplier;


        }
        public Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            return _context.SaveChangesAsync();

        }
    }
}