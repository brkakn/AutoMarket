using AutoMarket.Api.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCartEntity>
    {
        Task<ShoppingCartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default);
        Task<ShoppingCartEntity> GetByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default);
    }
}
