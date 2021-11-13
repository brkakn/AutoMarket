using AutoMarket.Api.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories.Interfaces
{
    public interface IShoppingCartDetailRepository : IGenericRepository<ShoppingCartDetailEntity>
    {
        Task CreateAsync(ShoppingCartDetailEntity shoppingCartDetailEntity, CancellationToken ct = default);
    }
}
