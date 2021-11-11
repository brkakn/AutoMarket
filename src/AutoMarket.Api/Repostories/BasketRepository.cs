using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class BasketRepository : GenericRepository<BasketEntity>, IBasketRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public BasketRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
