using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class ItemRepository : GenericRepository<ItemRepository>, IItemRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ItemRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
