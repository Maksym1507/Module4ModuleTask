using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<int> AddCustomerAsync(Customer customer);

        Task<Customer> GetCustomerAsync(int id);

        Task<Customer> GetCustomerWithOrdersAsync(int id);

        Task<bool> UpdateCustomerAsync(int id, Customer category);

        Task<bool> DeleteCustomerAsync(int id);
    }
}
