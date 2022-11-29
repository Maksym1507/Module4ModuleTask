using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShipperRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddShippertAsync(Shipper shipper)
        {
            var result = await _dbContext.Shippers.AddAsync(new ShipperEntity()
            {
                Name = shipper.Name,
                Phone = shipper.Phone,
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<ShipperEntity?> GetShipperAsync(int id)
        {
            return await _dbContext.Shippers.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<ShipperEntity?> GetShipperWithOrdersAsync(int id)
        {
            return await _dbContext.Shippers.Include(i => i.Orders).ThenInclude(t => t.OrderDetails).Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateShipperAsync(ShipperEntity shipper)
        {
            _dbContext.Shippers.Update(shipper);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteShipperAsync(ShipperEntity shipper)
        {
            _dbContext.Shippers.Remove(shipper);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
