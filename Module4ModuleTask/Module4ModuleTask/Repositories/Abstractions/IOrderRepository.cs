using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(Order order, List<OrderItem> items);

        Task<OrderEntity?> GetOrderAsync(int id);

        Task<IEnumerable<OrderEntity>?> GetOrdersByShipperIdAndPaymentIdAsync(int shipperId, int paymentId);

        Task<bool> UpdateOrderAsync(OrderEntity order);

        Task<bool> DeleteOrderAsync(OrderEntity order);
    }
}
