using System.Linq;

namespace WebShop.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
