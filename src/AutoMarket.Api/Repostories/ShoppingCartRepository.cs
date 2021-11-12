using AutoMarket.Api.Entities;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCartEntity>, IShoppingCartRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public ShoppingCartRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
