using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCartEntity>, IShoppingCartRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ShoppingCartRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingCartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking,
                    ct,
                    x => x.ShoppingCartDetails);
        }

        public async Task<ShoppingCartEntity> GetByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await GetAsync(
                    x =>
                    x.UserId == userId &&
                    x.Status == RecordStatuses.ACTIVE,
                    hasTracking,
                    ct);
        }
    }
}
