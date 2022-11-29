using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<int> AddProductAsync(Product product);

        Task<ProductEntity?> GetProductAsync(int id);

        Task<IEnumerable<ProductEntity>?> GetProductsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<ProductEntity>?> GetProductsByCategoryIdAndSupplierIdAsync(int categoryId, int supplierId);

        Task<bool> UpdateProductAsync(ProductEntity product);

        Task<bool> DeleteProductAsync(ProductEntity product);

        Task<bool> DeleteProductsByCategoryAsync(List<ProductEntity> products);
    }
}
