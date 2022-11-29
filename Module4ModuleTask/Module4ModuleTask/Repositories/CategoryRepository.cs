using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            var result = await _dbContext.Categories.AddAsync(new CategoryEntity()
            {
                Name = category.Name,
                Description = category.Description,
                Active = category.Active,
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<CategoryEntity?> GetCategoryAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> UpdateCategoryAsync(CategoryEntity categoryToUpdate)
        {
            _dbContext.Categories.Update(categoryToUpdate);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCategoryAsync(CategoryEntity categoryToDelete)
        {
            _dbContext.Categories.Remove(categoryToDelete);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
