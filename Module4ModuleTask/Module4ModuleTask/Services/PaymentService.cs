using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class PaymentService : BaseDateService<ApplicationDbContext>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public PaymentService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            IPaymentRepository paymentRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _paymentRepository = paymentRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _paymentRepository.AddPaymentAsync(payment);
                _loggerService.LogInformation($"Created payment with Id = {id}");
                return id;
            });
        }

        public async Task<Payment?> GetPaymentAsync(int id)
        {
            var result = await _paymentRepository.GetPaymentAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded payment with Id = {id}");
                return null!;
            }

            return new Payment()
            {
                Id = result.Id,
                PaymentType = result.PaymentType,
                Allowed = result.Allowed,
            };
        }

        public async Task<bool> UpdatePaymentAsync(int id, Payment payment)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var paymentToUpdate = await _paymentRepository.GetPaymentAsync(id);

                if (paymentToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded payment with Id = {id}");
                    return false;
                }

                paymentToUpdate.PaymentType = payment.PaymentType;
                paymentToUpdate.Allowed = payment.Allowed;

                return await _paymentRepository.UpdatePaymentAsync(paymentToUpdate!);
            });
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var paymentToDelete = await _paymentRepository.GetPaymentAsync(id);

                if (paymentToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded customer with Id = {id}");
                    return false;
                }

                return await _paymentRepository.DeletePaymentAsync(paymentToDelete!);
            });
        }
    }
}
