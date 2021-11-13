using AutoMarket.Api.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IItemRepository : IGenericRepository<ItemEntity>
    {
        Task<decimal> GetItemPriceAsync(long itemId, CancellationToken ct = default);
    }
}
