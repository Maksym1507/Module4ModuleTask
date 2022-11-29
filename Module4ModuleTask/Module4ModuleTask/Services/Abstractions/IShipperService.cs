using Module4ModuleTask.Models;

namespace Module4ModuleTask.Services.Abstractions
{
    public interface IShipperService
    {
        Task<int> AddShippertAsync(Shipper shipper);

        Task<Shipper> GetShipperAsync(int id);

        Task<Shipper> GetShipperWithOrdersAsync(int id);

        Task<bool> UpdateShipperAsync(int id, Shipper shipper);

        Task<bool> DeleteShipperAsync(int id);
    }
}
