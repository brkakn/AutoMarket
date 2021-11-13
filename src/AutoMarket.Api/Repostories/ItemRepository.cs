using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ItemRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> GetItemPriceAsync(long itemId, CancellationToken ct = default)
        {
            return await Query()
                .Where(
                    x =>
                    x.Id == itemId &&
                    x.Status == RecordStatuses.ACTIVE)
                .Select(x => x.Price)
                .FirstOrDefaultAsync(ct);
        }
    }
}
