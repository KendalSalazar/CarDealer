using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using System.Linq.Expressions;

namespace CarDealer.Data.Repository
{
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {
        private ApplicationDbContext db;

        public VehicleModelRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(VehicleModel vehicleModel)
        {
            db.VehicleModels.Update(vehicleModel);
        }
    }
}
