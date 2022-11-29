using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;
namespace Module4ModuleTask.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddSupplierAsync(Supplier supplier)
        {
            var result = await _dbContext.Suppliers.AddAsync(new SupplierEntity()
            {
                CompanyName = supplier.CompanyName,
                Address = supplier.Address,
                City = supplier.City,
                State = supplier.State,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
                Email = supplier.Email,
                DiscountType = supplier.DiscountType,
                Notes = supplier.Notes,
                DiscountAvailable = supplier.DiscountAvailable
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<SupplierEntity?> GetSupplierAsync(int id)
        {
            return await _dbContext.Suppliers.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<SupplierEntity?> GetSupplierWithProductsAsync(int id)
        {
            return await _dbContext.Suppliers.Include(i => i.Products).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSupplierAsync(SupplierEntity supplier)
        {
            _dbContext.Suppliers.Update(supplier);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteSupplierAsync(SupplierEntity supplier)
        {
            _dbContext.Suppliers.Remove(supplier);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
