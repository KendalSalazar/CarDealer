using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using System.Linq.Expressions;

namespace CarDealer.Data.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private ApplicationDbContext db;

        public VehicleRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Vehicle vehicle)
        {
            db.Vehicles.Update(vehicle);
        }
    }
}
