using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ItemRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
