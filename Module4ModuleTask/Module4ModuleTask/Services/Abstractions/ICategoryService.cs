using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<int> AddCategoryAsync(Category category);

        Task<Category> GetCategoryAsync(int id);

        Task<bool> UpdateCategoryAsync(int id, Category category);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
