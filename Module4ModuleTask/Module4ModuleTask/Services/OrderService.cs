using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class OrderService : BaseDateService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public OrderService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            IOrderRepository orderRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _orderRepository = orderRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddOrderAsync(Order order, List<OrderItem> items)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _orderRepository.AddOrderAsync(order, items);
                _loggerService.LogInformation($"Created order with Id = {id}");
                return id;
            });
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var result = await _orderRepository.GetOrderAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded order with Id = {id}");
                return null!;
            }

            return new Order()
            {
                Id = result.Id,
                CustomerId = result.CustomerId,
                PaymentId = result.PaymentId,
                ShipDate = result.ShipDate,
                RequiredDate = result.RequiredDate,
                ShipperId = result.ShipperId,
                SalesTax = result.SalesTax,
                TransactStatus = result.TransactStatus,
                Fulfilled = result.Fulfilled,
                Deleted = result.Deleted,
                Paid = result.Paid,
                PaymentDate = result.PaymentDate,
                OrderDetails = result.OrderDetails.Select(s => new OrderItem()
                {
                    Id = s.Id,
                    OrderId = s.OrderId,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                })
            };
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByShipperIdAndPaymentIdAsync(int shipperId, int paymentId)
        {
            var result = await _orderRepository.GetOrdersByShipperIdAndPaymentIdAsync(shipperId, paymentId);

            if (result!.Count() == 0)
            {
                _loggerService.LogWarning($"Not founded order with shipper Id = {shipperId} and payment id = {paymentId}");
                return null!;
            }

            return result!.Select(s => new Order()
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                PaymentId = s.PaymentId,
                Payment = new Payment()
                {
                    Id = s.Id,
                    PaymentType = s.Payment!.PaymentType,
                    Allowed = s.Payment.Allowed
                },
                ShipDate = s.ShipDate,
                RequiredDate = s.RequiredDate,
                ShipperId = s.ShipperId,
                Shipper = new Shipper()
                {
                    Id = s.Shipper!.Id,
                    Name = s.Shipper.Name,
                    Phone = s.Shipper.Phone,
                },
                SalesTax = s.SalesTax,
                TransactStatus = s.TransactStatus,
                Fulfilled = s.Fulfilled,
                Deleted = s.Deleted,
                Paid = s.Paid,
                PaymentDate = s.PaymentDate,
                OrderDetails = s.OrderDetails.Select(s => new OrderItem()
                {
                    Id = s.Id,
                    OrderId = s.OrderId,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                })
            }).ToList();
        }

        public async Task<bool> UpdateOrderAsync(int id, Order order)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderToUpdate = await _orderRepository.GetOrderAsync(id);

                if (orderToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded order with Id = {id}");
                    return false;
                }

                orderToUpdate.CustomerId = order.CustomerId;
                orderToUpdate.PaymentId = order.PaymentId;
                orderToUpdate.ShipDate = order.ShipDate;
                orderToUpdate.RequiredDate = order.RequiredDate;
                orderToUpdate.ShipperId = order.ShipperId;
                orderToUpdate.SalesTax = order.SalesTax;
                orderToUpdate.TransactStatus = order.TransactStatus;
                orderToUpdate.Fulfilled = order.Fulfilled;
                orderToUpdate.Deleted = order.Deleted;
                orderToUpdate.Paid = order.Paid;
                orderToUpdate.PaymentDate = order.PaymentDate;

                var orderDetailsToUpdate = order.OrderDetails!.ToList();
                int orderDetailsCounter = 0;

                foreach (var item in orderToUpdate.OrderDetails)
                {
                    item.ProductId = orderDetailsToUpdate[orderDetailsCounter].ProductId;
                    item.Quantity = orderDetailsToUpdate[orderDetailsCounter].Quantity;

                    orderDetailsCounter++;
                }

                var isUpdated = await _orderRepository.UpdateOrderAsync(orderToUpdate!);
                _loggerService.LogInformation($"Updated order with Id = {id}");
                return isUpdated;
            });
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderToDelete = await _orderRepository.GetOrderAsync(id);

                if (orderToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded order with Id = {id}");
                    return false;
                }

                var isDeleted = await _orderRepository.DeleteOrderAsync(orderToDelete!);
                _loggerService.LogInformation($"Removed order with Id = {id}");
                return isDeleted;
            });
        }
    }
}
