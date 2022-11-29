using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(Customer customer);

        Task<CustomerEntity?> GetCustomerAsync(int id);

        Task<CustomerEntity?> GetCustomerWithOrdersAsync(int id);

        Task<bool> UpdateCustomerAsync(CustomerEntity customer);

        Task<bool> DeleteCustomerAsync(CustomerEntity customer);
    }
}
