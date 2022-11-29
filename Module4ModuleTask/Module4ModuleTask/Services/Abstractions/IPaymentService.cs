using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface IPaymentService
    {
        Task<int> AddPaymentAsync(Payment payment);

        Task<Payment?> GetPaymentAsync(int id);

        Task<bool> UpdatePaymentAsync(int id, Payment payment);

        Task<bool> DeletePaymentAsync(int id);
    }
}
