using AutoMarket.Api.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IStockRepository : IGenericRepository<StockEntity>
    {
        Task<StockEntity> GetByItemId(long itemId, bool hasTracking = true, CancellationToken ct = default);
    }
}
