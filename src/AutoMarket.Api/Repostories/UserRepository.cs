using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;

namespace AutoMarket.Api.Repostories
{
    public class UserRepository : GenericRepository<UserRepository>, IUserRepository
    {
        private readonly AutoMarketDbContext _dbContext;

        public UserRepository(AutoMarketDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
