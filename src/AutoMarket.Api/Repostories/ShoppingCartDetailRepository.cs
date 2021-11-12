using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class ShoppingCartDetailRepository : GenericRepository<ShoppingCartDetailEntity>, IShoppingCartDetailRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ShoppingCartDetailRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
