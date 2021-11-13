using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class StockRepository : GenericRepository<StockEntity>, IStockRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public StockRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockEntity> GetByItemId(long itemId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await this.GetAsync(
                x =>
                x.ItemId == itemId &&
                x.Status == RecordStatuses.ACTIVE, hasTracking, ct);
        }
    }
}
