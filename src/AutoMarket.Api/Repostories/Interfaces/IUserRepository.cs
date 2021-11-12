using AutoMarket.Api.Entities;
using AutoMarket.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserModel> AuthenticateUser(string userName, string password, CancellationToken ct = default);
    }
}
