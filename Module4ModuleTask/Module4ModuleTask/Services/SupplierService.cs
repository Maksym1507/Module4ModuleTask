using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class SupplierService : BaseDateService<ApplicationDbContext>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public SupplierService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            ISupplierRepository supplierRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _supplierRepository = supplierRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddSupplierAsync(Supplier supplier)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _supplierRepository.AddSupplierAsync(supplier);
                _loggerService.LogInformation($"Created supplier with Id = {id}");
                return id;
            });
        }

        public async Task<Supplier> GetSupplierAsync(int id)
        {
            var result = await _supplierRepository.GetSupplierAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded supplier with Id = {id}");
                return null!;
            }

            return new Supplier()
            {
                Id = result.Id,
                CompanyName = result.CompanyName,
                Address = result.Address,
                City = result.City,
                State = result.State,
                PostalCode = result.PostalCode,
                Country = result.Country,
                Phone = result.Phone,
                Fax = result.Fax,
                Email = result.Email,
                DiscountType = result.DiscountType,
                Notes = result.Notes,
                DiscountAvailable = result.DiscountAvailable
            };
        }

        public async Task<Supplier> GetSupplierWithProductsAsync(int id)
        {
            var result = await _supplierRepository.GetSupplierWithProductsAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded supplier with Id = {id}");
                return null!;
            }

            return new Supplier()
            {
                Id = result.Id,
                CompanyName = result.CompanyName,
                Address = result.Address,
                City = result.City,
                State = result.State,
                PostalCode = result.PostalCode,
                Country = result.Country,
                Phone = result.Phone,
                Fax = result.Fax,
                Email = result.Email,
                DiscountType = result.DiscountType,
                Notes = result.Notes,
                DiscountAvailable = result.DiscountAvailable,
                Products = result.Products.Select(s => new Product()
                {
                    Id = s.Id,
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
                }).ToList()
            };
        }

        public async Task<bool> UpdateSupplierAsync(int id, Supplier supplier)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var supplierToUpdate = await _supplierRepository.GetSupplierAsync(id);

                if (supplierToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded supplier with Id = {id}");
                    return false;
                }

                supplierToUpdate.CompanyName = supplier.CompanyName;
                supplierToUpdate.Address = supplier.Address;
                supplierToUpdate.City = supplier.City;
                supplierToUpdate.State = supplier.State;
                supplierToUpdate.PostalCode = supplier.PostalCode;
                supplierToUpdate.Country = supplier.Country;
                supplierToUpdate.Phone = supplier.Phone;
                supplierToUpdate.Fax = supplier.Fax;
                supplierToUpdate.Email = supplier.Email;
                supplierToUpdate.DiscountType = supplier.DiscountType;
                supplierToUpdate.Notes = supplier.Notes;
                supplierToUpdate.DiscountAvailable = supplier.DiscountAvailable;

                return await _supplierRepository.UpdateSupplierAsync(supplierToUpdate!);
            });
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var supplierToDelete = await _supplierRepository.GetSupplierAsync(id);

                if (supplierToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded supplier with Id = {id}");
                    return false;
                }

                return await _supplierRepository.DeleteSupplierAsync(supplierToDelete!);
            });
        }
    }
}
