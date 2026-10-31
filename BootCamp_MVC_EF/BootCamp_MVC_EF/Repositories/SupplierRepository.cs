using BootCamp_MVC_EF.Data;
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

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
