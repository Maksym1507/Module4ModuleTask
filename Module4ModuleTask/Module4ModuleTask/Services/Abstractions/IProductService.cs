using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface IProductService
    {
        Task<int> AddProductAsync(Product product);

        Task<Product> GetProductAsync(int id);

        Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(int categoryId);

        Task<IReadOnlyList<Product>> GetProductsByCategoryIdAndSupplierIdAsync(int categoryId, int supplierId);

        Task<bool> UpdateProductAsync(int id, Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<bool> DeleteProductsByCategoryIdAsync(int categoryId);
    }
}
