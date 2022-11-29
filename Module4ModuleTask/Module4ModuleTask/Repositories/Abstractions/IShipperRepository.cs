using Module4ModuleTask.Data.Entities;
using Module4ModuleTask.Models;

namespace Module4ModuleTask.Repositories.Abstractions
{
    public interface IShipperRepository
    {
        Task<int> AddShippertAsync(Shipper shipper);

        Task<ShipperEntity?> GetShipperAsync(int id);

        Task<ShipperEntity?> GetShipperWithOrdersAsync(int id);

        Task<bool> UpdateShipperAsync(ShipperEntity shipper);

        Task<bool> DeleteShipperAsync(ShipperEntity shipper);
    }
}
