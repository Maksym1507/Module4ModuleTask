using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class CategoryService : BaseDateService<ApplicationDbContext>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public CategoryService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            ICategoryRepository categoryRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _categoryRepository = categoryRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _categoryRepository.AddCategoryAsync(category);
                _loggerService.LogInformation($"Created category with Id = {id}");
                return id;
            });
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var result = await _categoryRepository.GetCategoryAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded category with Id = {id}");
                return null!;
            }

            return new Category()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Active = result.Active,
            };
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var categoryToUpdate = await _categoryRepository.GetCategoryAsync(id);

                if (categoryToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded category with Id = {id}");
                    return false;
                }

                categoryToUpdate!.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                categoryToUpdate.Active = category.Active;

                return await _categoryRepository.UpdateCategoryAsync(categoryToUpdate!);
            });
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var categoryToDelete = await _categoryRepository.GetCategoryAsync(id);

                if (categoryToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded category with Id = {id}");
                    return false;
                }

                return await _categoryRepository.DeleteCategoryAsync(categoryToDelete!);
            });
        }
    }
}
