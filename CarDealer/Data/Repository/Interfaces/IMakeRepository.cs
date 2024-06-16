using CarDealer.Models;

namespace CarDealer.Data.Repository.Interfaces
{
    public interface IMakeRepository : IRepository<Make>
    {
        void Update(Make make);

    }
}
