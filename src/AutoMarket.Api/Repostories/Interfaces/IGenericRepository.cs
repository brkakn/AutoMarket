using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
