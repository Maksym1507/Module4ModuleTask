using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface IPaymentRepository
    {
        Task<int> AddPaymentAsync(Payment payment);

        Task<PaymentEntity?> GetPaymentAsync(int id);

        Task<bool> UpdatePaymentAsync(PaymentEntity payment);

        Task<bool> DeletePaymentAsync(PaymentEntity payment);
    }
}
