using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class BasketDetailRepository : GenericRepository<BasketDetailRepository>, IBasketDetailRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public BasketDetailRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
