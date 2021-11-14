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
    public class ShoppingCartRepository : GenericRepository<ShoppingCartEntity>, IShoppingCartRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ShoppingCartRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingCartEntity> GetWithDetailByUserIdAsync(long userId, bool hasTracking = true, CancellationToken ct = default)
        {
            return await Query()
                .Include(e => e.ShoppingCartDetails)
                .ThenInclude(e => e.Item)
                .Where(e => e.UserId == userId && e.Status == RecordStatuses.ACTIVE)
                .FirstOrDefaultAsync(ct);
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
