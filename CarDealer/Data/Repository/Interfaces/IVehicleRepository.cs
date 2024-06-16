using CarDealer.Models;

namespace CarDealer.Data.Repository.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        void Update(Vehicle vehicle);

    }
}
