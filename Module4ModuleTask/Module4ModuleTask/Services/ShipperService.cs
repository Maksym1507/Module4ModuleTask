using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class ShipperService : BaseDateService<ApplicationDbContext>, IShipperService
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public ShipperService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            IShipperRepository shipperRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _shipperRepository = shipperRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddShippertAsync(Shipper shipper)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _shipperRepository.AddShippertAsync(shipper);
                _loggerService.LogInformation($"Created shipper with Id = {id}");
                return id;
            });
        }

        public async Task<Shipper> GetShipperAsync(int id)
        {
            var result = await _shipperRepository.GetShipperAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded shipper with Id = {id}");
                return null!;
            }

            return new Shipper()
            {
                Id = id,
                Name = result.Name,
                Phone = result.Phone,
            };
        }

        public async Task<Shipper> GetShipperWithOrdersAsync(int id)
        {
            var result = await _shipperRepository.GetShipperWithOrdersAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded customer with Id = {id}");
                return null!;
            }

            return new Shipper()
            {
                Id = id,
                Name = result.Name,
                Phone = result.Phone,
                Orders = result.Orders.Select(s => new Order()
                {
                    CustomerId = s.CustomerId,
                    PaymentId = s.PaymentId,
                    ShipDate = s.ShipDate,
                    RequiredDate = s.RequiredDate,
                    ShipperId = s.ShipperId,
                    SalesTax = s.SalesTax,
                    TransactStatus = s.TransactStatus,
                    Fulfilled = s.Fulfilled,
                    Deleted = s.Deleted,
                    Paid = s.Paid,
                    PaymentDate = s.PaymentDate,
                    OrderDetails = s.OrderDetails.Select(s => new OrderItem()
                    {
                        Id = s.Id,
                        ProductId = s.ProductId,
                        Quantity = s.Quantity,
                    })
                }).ToList()
            };
        }

        public async Task<bool> UpdateShipperAsync(int id, Shipper shipper)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var shipperToUpdate = await _shipperRepository.GetShipperAsync(id);

                if (shipperToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded shipper with Id = {id}");
                    return false;
                }

                shipperToUpdate.Name = shipper.Name;
                shipperToUpdate.Phone = shipper.Phone;

                return await _shipperRepository.UpdateShipperAsync(shipperToUpdate!);
            });
        }

        public async Task<bool> DeleteShipperAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var shipperToDelete = await _shipperRepository.GetShipperAsync(id);

                if (shipperToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded shipper with Id = {id}");
                    return false;
                }

                return await _shipperRepository.DeleteShipperAsync(shipperToDelete!);
            });
        }
    }
}
