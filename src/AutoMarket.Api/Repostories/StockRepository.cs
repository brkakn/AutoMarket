using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class StockRepository : GenericRepository<StockEntity>, IStockRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public StockRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
