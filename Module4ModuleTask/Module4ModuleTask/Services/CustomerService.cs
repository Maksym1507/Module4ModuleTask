using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Services
{
    public class CustomerService : BaseDateService<ApplicationDbContext>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _loggerService;

        public CustomerService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDateService<ApplicationDbContext>> logger,
            ICustomerRepository customerRepository,
            ILogger<CustomerService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _customerRepository = customerRepository;
            _loggerService = loggerService;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = await _customerRepository.AddCustomerAsync(customer);
                _loggerService.LogInformation($"Created customer with Id = {id}");
                return id;
            });
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var result = await _customerRepository.GetCustomerAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded customer with Id = {id}");
                return null!;
            }

            return new Customer()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Class = result.Class,
                Room = result.Room,
                Building = result.Building,
                Address = result.Address,
                City = result.City,
                State = result.State,
                PostalCode = result.PostalCode,
                Country = result.Country,
                Phone = result.Phone,
                Email = result.Email,
                Password = result.Password,
                CreditCard = result.CreditCard,
            };
        }

        public async Task<Customer> GetCustomerWithOrdersAsync(int id)
        {
            var result = await _customerRepository.GetCustomerWithOrdersAsync(id);

            if (result == null)
            {
                _loggerService.LogWarning($"Not founded customer with Id = {id}");
                return null!;
            }

            return new Customer()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Class = result.Class,
                Room = result.Room,
                Building = result.Building,
                Address = result.Address,
                City = result.City,
                State = result.State,
                PostalCode = result.PostalCode,
                Country = result.Country,
                Phone = result.Phone,
                Email = result.Email,
                Password = result.Password,
                CreditCard = result.CreditCard,
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

        public async Task<bool> UpdateCustomerAsync(int id, Customer customer)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var customerToUpdate = await _customerRepository.GetCustomerAsync(id);

                if (customerToUpdate == null)
                {
                    _loggerService.LogWarning($"Not founded customer with Id = {id}");
                    return false;
                }

                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.Class = customer.Class;
                customerToUpdate.Room = customer.Room;
                customerToUpdate.Building = customer.Building;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.City = customer.City;
                customerToUpdate.State = customer.State;
                customerToUpdate.PostalCode = customer.PostalCode;
                customerToUpdate.Country = customer.Country;
                customerToUpdate.Phone = customer.Phone;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.Password = customer.Password;
                customerToUpdate.CreditCard = customer.CreditCard;

                return await _customerRepository.UpdateCustomerAsync(customerToUpdate!);
            });
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var customerToDelete = await _customerRepository.GetCustomerAsync(id);

                if (customerToDelete == null)
                {
                    _loggerService.LogWarning($"Not founded customer with Id = {id}");
                    return false;
                }

                return await _customerRepository.DeleteCustomerAsync(customerToDelete!);
            });
        }
    }
}
