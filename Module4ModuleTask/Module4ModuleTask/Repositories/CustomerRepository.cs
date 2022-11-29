using Microsoft.EntityFrameworkCore;
using Module4ModuleTask.Data;
using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            var result = await _dbContext.Customers.AddAsync(new CustomerEntity()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Class = customer.Class,
                Room = customer.Room,
                Building = customer.Building,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Email = customer.Email,
                Password = customer.Password,
                CreditCard = customer.CreditCard,
            });

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<CustomerEntity?> GetCustomerAsync(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<CustomerEntity?> GetCustomerWithOrdersAsync(int id)
        {
            return await _dbContext.Customers.Include(i => i.Orders).ThenInclude(t => t.OrderDetails).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCustomerAsync(CustomerEntity customer)
        {
            _dbContext.Customers.Update(customer);

            var quantityEntriesUpdated = await _dbContext.SaveChangesAsync();

            if (quantityEntriesUpdated > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCustomerAsync(CustomerEntity customer)
        {
            _dbContext.Customers.Remove(customer);

            var quantityEntriesDeleted = await _dbContext.SaveChangesAsync();

            if (quantityEntriesDeleted > 0)
            {
                return true;
            }

            return false;
        }
    }
}
