using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface ISupplierRepository
    {
        Task<int> AddSupplierAsync(Supplier supplier);

        Task<SupplierEntity?> GetSupplierAsync(int id);

        Task<SupplierEntity?> GetSupplierWithProductsAsync(int id);

        Task<bool> UpdateSupplierAsync(SupplierEntity supplier);

        Task<bool> DeleteSupplierAsync(SupplierEntity supplier);
    }
}
