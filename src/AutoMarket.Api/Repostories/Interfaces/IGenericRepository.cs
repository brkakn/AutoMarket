using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<IList<T>> GetAllAsync(bool hasTracking = false, CancellationToken ct = default);
        Task<T> GetAsync(int id, CancellationToken ct = default);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool hasTracking = false, CancellationToken ct = default);
        Task<bool> ExistAsync(int id, CancellationToken ct = default);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
