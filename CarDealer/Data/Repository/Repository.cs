using CarDealer.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace CarDealer.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db; //readonly es para que no modifique en ejecucion
        internal DbSet<T> dbSet; //El dbSet va a ser del mismo tipo que el repositorio<T>
        //El dbSet representa una tabla en la base de datos
        //linq es un lenguaje parecido a sql que permite hacer selects, where, etc en el codigo (C#)

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null) //Expression es como un metodo que recibe por parametro otro metodo
        { //includeProperties es por si ese objeto de la bd posee otro objeto
            IQueryable<T> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            query = query.Where(filter); //Se filtra por lo que recibe por parametro
            return query.FirstOrDefault(); //Retorna solo uno
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.ToList(); //Retorna uno o muchos
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
