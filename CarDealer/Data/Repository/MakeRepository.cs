using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using System.Linq.Expressions;

namespace CarDealer.Data.Repository
{
    public class MakeRepository : Repository<Make>, IMakeRepository
    {
        private ApplicationDbContext db;

        public MakeRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Make make)
        {
            db.Makes.Update(make);
        }
    }
}
