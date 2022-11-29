using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface ISupplierService
    {
        Task<int> AddSupplierAsync(Supplier supplier);

        Task<Supplier> GetSupplierAsync(int id);

        Task<Supplier> GetSupplierWithProductsAsync(int id);

        Task<bool> UpdateSupplierAsync(int id, Supplier supplier);

        Task<bool> DeleteSupplierAsync(int id);
    }
}
