using AutoMapper;
using AutoMarket.Api.Entities;
using AutoMarket.Api.Helpers;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Models;
using AutoMarket.Api.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly AutoMarketDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AutoMarketDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserModel> AuthenticateUser(string userName, string password, CancellationToken ct = default)
        {
            var userEntity = await _dbContext.Users.Where(e => e.UserName == userName && e.Password == SecurityHelper.ComputeSha256Hash(password)).FirstOrDefaultAsync(ct);

            return userEntity == null ? null : _mapper.Map<UserModel>(userEntity);
        }
    }
}
