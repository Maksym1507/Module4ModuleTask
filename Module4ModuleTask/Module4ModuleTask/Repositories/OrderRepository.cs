using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddOrderAsync(Order order, List<OrderItem> items)
        {
            var result = await _dbContext.Orders.AddAsync(new OrderEntity()
            {
                CustomerId = order.CustomerId,
                PaymentId = order.PaymentId,
                ShipDate = order.ShipDate,
                OrderDate = DateTime.UtcNow,
                RequiredDate = order.RequiredDate,
                ShipperId = order.ShipperId,
                SalesTax = order.SalesTax,
                TransactStatus = order.TransactStatus,
                Fulfilled = order.Fulfilled,
                Deleted = order.Deleted,
                Paid = order.Paid,
                PaymentDate = order.PaymentDate
            });

            await _dbContext.OrderDetails.AddRangeAsync(items.Select(s => new OrderDetailsEntity()
            {
                OrderId = result.Entity.Id,
                ProductId = s.ProductId,
                Quantity = s.Quantity,
            }));

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<OrderEntity?> GetOrderAsync(int id)
        {
            return await _dbContext.Orders.Include(i => i.OrderDetails).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<OrderEntity>?> GetOrdersByShipperIdAndPaymentIdAsync(int shipperId, int paymentId)
        {
            return await _dbContext.Orders.Include(i => i.OrderDetails).Include(i => i.Shipper).Include(i => i.Payment).Where(w => w.ShipperId == shipperId && w.PaymentId == paymentId).ToListAsync();
        }

        public async Task<bool> UpdateOrderAsync(OrderEntity order)
        {
            _dbContext.OrderDetails.UpdateRange(order.OrderDetails);

            var quantityOrderDetailsUpdated = await _dbContext.SaveChangesAsync();

            _dbContext.Orders.Update(order);

            var quantityOrdersUpdated = await _dbContext.SaveChangesAsync();

            if (quantityOrderDetailsUpdated > 0 && quantityOrdersUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteOrderAsync(OrderEntity order)
        {
            _dbContext.OrderDetails.RemoveRange(order.OrderDetails);

            var quantityOrderDetailsDeleted = await _dbContext.SaveChangesAsync();

            _dbContext.Orders.Remove(order);

            var quantityOrdersDeleted = await _dbContext.SaveChangesAsync();

            if (quantityOrderDetailsDeleted > 0 && quantityOrdersDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
