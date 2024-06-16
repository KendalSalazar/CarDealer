namespace CarDealer.Data.Repository.Interfaces
{
    public interface IUnitOfWork //Tiene la lista de todos los repositorios y el save
    {
        IMakeRepository Make { get; }
        IVehicleModelRepository VehicleModel { get; }

        IVehicleRepository vehicle { get; }
        void Save();
    }
}
