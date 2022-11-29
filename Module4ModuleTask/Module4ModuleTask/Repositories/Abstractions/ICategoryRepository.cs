using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface ICategoryRepository
    {
        Task<int> AddCategoryAsync(Category category);

        Task<CategoryEntity?> GetCategoryAsync(int id);

        Task<bool> UpdateCategoryAsync(CategoryEntity categoryToUpdate);

        Task<bool> DeleteCategoryAsync(CategoryEntity categoryToDelete);
    }
}
