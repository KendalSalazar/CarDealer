using CarDealer.Data.Repository.Interfaces;

namespace CarDealer.Data.Repository
{
    public class UnitOfWork : IUnitOfWork //Es el encargado de acceder a la BD
    {
        private ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            Make = new MakeRepository(db);
            VehicleModel = new VehicleModelRepository(db);
            vehicle = new VehicleRepository(db);
        }

        public IMakeRepository Make { get; private set; }

        public IVehicleModelRepository VehicleModel { get; private set; }

        public IVehicleRepository vehicle { get; private set; }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
