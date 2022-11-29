using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(Order order, List<OrderItem> items);

        Task<Order> GetOrderAsync(int id);

        Task<IReadOnlyList<Order>> GetOrdersByShipperIdAndPaymentIdAsync(int shipperId, int paymentId);

        Task<bool> UpdateOrderAsync(int id, Order order);

        Task<bool> DeleteOrderAsync(int id);
    }
}
