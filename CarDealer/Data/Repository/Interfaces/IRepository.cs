using System.Linq.Expressions;

namespace CarDealer.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression <Func<T, bool>> filter, string? includeProperties = null);
        
        IEnumerable<T> GetAll(string? includeProperties = null);

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities); //Puede recibir una lista 

        //IEnumerable: Clase padre de casi cualquier lista

        //Lazy loading: Solo se carga lo minimo necesario
        //Eager loading: Carga todos los datos, accede a todos a nivel de rendimiento



    }
}
