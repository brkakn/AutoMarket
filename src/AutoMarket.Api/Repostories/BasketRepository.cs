using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class BasketRepository : GenericRepository<BasketRepository>, IBasketRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public BasketRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
