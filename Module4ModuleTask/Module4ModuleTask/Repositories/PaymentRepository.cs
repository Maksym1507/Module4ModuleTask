using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            var result = await _dbContext.Payments.AddAsync(new PaymentEntity()
            {
                PaymentType = payment.PaymentType,
                Allowed = payment.Allowed,
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<PaymentEntity?> GetPaymentAsync(int id)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> UpdatePaymentAsync(PaymentEntity payment)
        {
            _dbContext.Payments.Update(payment);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePaymentAsync(PaymentEntity payment)
        {
            _dbContext.Payments.Remove(payment);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
