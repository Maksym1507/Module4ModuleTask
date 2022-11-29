using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var result = await _dbContext.Products.AddAsync(new ProductEntity()
            {
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                Size = product.Size,
                Color = product.Color,
                Discount = product.Discount,
                Picture = product.Picture,
                Ranking = product.Ranking,
                Note = product.Note,
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<ProductEntity?> GetProductAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<ProductEntity>?> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products.Include(i => i.Category).Where(w => w.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<ProductEntity>?> GetProductsByCategoryIdAndSupplierIdAsync(int categoryId, int supplierId)
        {
            return await _dbContext.Products.Include(i => i.Category).Include(i => i.Supplier).Where(w => w.CategoryId == categoryId && w.SupplierId == supplierId).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            _dbContext.Products.Update(product);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteProductAsync(ProductEntity product)
        {
            _dbContext.Products.Remove(product);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteProductsByCategoryAsync(List<ProductEntity> products)
        {
            _dbContext.Products.RemoveRange(products);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
