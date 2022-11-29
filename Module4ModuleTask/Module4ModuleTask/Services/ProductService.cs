using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class ProductService : BaseDateService<ApplicationDbContext>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public ProductService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            IProductRepository productRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _productRepository = productRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _productRepository.AddProductAsync(product);
                _loggerService.LogInformation($"Created product with Id = {id}");
                return id;
            });
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var result = await _productRepository.GetProductAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded product with Id = {id}");
                return null!;
            }

            return new Product()
            {
                Id = result.Id,
                SKU = result.SKU,
                Name = result.Name,
                Description = result.Description,
                SupplierId = result.SupplierId,
                CategoryId = result.CategoryId,
                QuantityPerUnit = result.QuantityPerUnit,
                UnitPrice = result.UnitPrice,
                Size = result.Size,
                Color = result.Color,
                Discount = result.Discount,
                Picture = result.Picture,
                Ranking = result.Ranking,
                Note = result.Note,
            };
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var result = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded products with category Id = {categoryId}");
                return null!;
            }

            return result.Select(s => new Product()
            {
                SKU = s.SKU,
                Name = s.Name,
                Description = s.Description,
                SupplierId = s.SupplierId,
                CategoryId = s.CategoryId,
                QuantityPerUnit = s.QuantityPerUnit,
                UnitPrice = s.UnitPrice,
                Size = s.Size,
                Color = s.Color,
                Discount = s.Discount,
                Picture = s.Picture,
                Ranking = s.Ranking,
                Note = s.Note,
                Category = new Category()
                {
                    Id = s.Category!.Id,
                    Name = s.Category.Name,
                    Description = s.Category.Description,
                    Active = s.Category.Active,
                }
            }).ToList();
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryIdAndSupplierIdAsync(int categoryId, int supplierId)
        {
            var result = await _productRepository.GetProductsByCategoryIdAndSupplierIdAsync(categoryId, supplierId);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded products with category Id = {categoryId} and supplier id = {supplierId}");
                return null!;
            }

            return result.Select(s => new Product()
            {
                SKU = s.SKU,
                Name = s.Name,
                Description = s.Description,
                SupplierId = s.SupplierId,
                CategoryId = s.CategoryId,
                QuantityPerUnit = s.QuantityPerUnit,
                UnitPrice = s.UnitPrice,
                Size = s.Size,
                Color = s.Color,
                Discount = s.Discount,
                Picture = s.Picture,
                Ranking = s.Ranking,
                Note = s.Note,
                Supplier = new Supplier()
                {
                    Id = s.Supplier!.Id,
                    CompanyName = s.Supplier.CompanyName,
                    Address = s.Supplier.Address,
                    City = s.Supplier.City,
                    State = s.Supplier.State,
                    PostalCode = s.Supplier.PostalCode,
                    Country = s.Supplier.Country,
                    Phone = s.Supplier.Phone,
                    Fax = s.Supplier.Fax,
                    Email = s.Supplier.Email,
                    DiscountType = s.Supplier.DiscountType,
                    Notes = s.Supplier.Notes,
                    DiscountAvailable = s.Supplier.DiscountAvailable
                },
                Category = new Category()
                {
                    Id = s.Category!.Id,
                    Name = s.Category.Name,
                    Description = s.Category.Description,
                    Active = s.Category.Active,
                }
            }).ToList();
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var productToUpdate = await _productRepository.GetProductAsync(id);

                if (productToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded product with Id = {id}");
                    return false;
                }

                productToUpdate.SKU = product.SKU;
                productToUpdate.Name = product.Name;
                productToUpdate.Description = product.Description;
                productToUpdate.SupplierId = product.SupplierId;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.QuantityPerUnit = product.QuantityPerUnit;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.Size = product.Size;
                productToUpdate.Color = product.Color;
                productToUpdate.Discount = product.Discount;
                productToUpdate.Picture = product.Picture;
                productToUpdate.Ranking = product.Ranking;
                productToUpdate.Note = product.Note;

                return await _productRepository.UpdateProductAsync(productToUpdate!);
            });
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var productToDelete = await _productRepository.GetProductAsync(id);

                if (productToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded product with Id = {id}");
                    return false;
                }

                return await _productRepository.DeleteProductAsync(productToDelete!);
            });
        }

        public async Task<bool> DeleteProductsByCategoryIdAsync(int categoryId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var productsToDelete = await _productRepository.GetProductsByCategoryIdAsync(categoryId);

                if (productsToDelete!.Count() == 0)
                {
                    _loggerService.LogWarning($"Not founded products with category Id = {categoryId}");
                    return false;
                }

                return await _productRepository.DeleteProductsByCategoryAsync(productsToDelete!.ToList());
            });
        }
    }
}
