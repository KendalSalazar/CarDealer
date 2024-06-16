using CarDealer.Models;

namespace CarDealer.Data.Repository.Interfaces
{
    public interface IVehicleModelRepository : IRepository<VehicleModel>
    {
        void Update(VehicleModel vehicleModel);

    }
}
