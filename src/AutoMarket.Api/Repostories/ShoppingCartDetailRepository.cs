using AutoMarket.Api.Entities;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Infrastructures.Database;
using AutoMarket.Api.Repostories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Repostories
{
    public class ShoppingCartDetailRepository : GenericRepository<ShoppingCartDetailEntity>, IShoppingCartDetailRepository
    {
        private readonly AutoMarketDbContext _dbContext;
        private readonly IItemRepository _itemRepository;

        public ShoppingCartDetailRepository(AutoMarketDbContext dbContext, IItemRepository itemRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _itemRepository = itemRepository;
        }

        public async Task CreateAsync(ShoppingCartDetailEntity shoppingCartDetailEntity, CancellationToken ct = default)
        {
            var currentCartDetail = await GetByCartIdAndItemId(shoppingCartDetailEntity.ShoppingCartId, shoppingCartDetailEntity.ItemId);
            if (currentCartDetail != null)
            {
                currentCartDetail.AddQuantity(shoppingCartDetailEntity.Quantity);
            }
            else
            {
                if (shoppingCartDetailEntity.ShoppingCartId == 0)
                    shoppingCartDetailEntity.ShoppingCart.Add();

                var price = shoppingCartDetailEntity.Item == null
                    ? await _itemRepository.GetItemPriceAsync(shoppingCartDetailEntity.ItemId, ct)
                    : shoppingCartDetailEntity.Item.Price;

                shoppingCartDetailEntity.ShoppingCart.AddAmount(shoppingCartDetailEntity.Quantity * price);
                await AddAsync(shoppingCartDetailEntity);
            }
        }

        public async Task UpdateAsync(ShoppingCartDetailEntity cartDetailEntity)
        {
            var currentCartDetail = await GetByCartIdAndItemId(cartDetailEntity.ShoppingCartId, cartDetailEntity.ItemId);
            if (currentCartDetail != null)
            {
                currentCartDetail.Quantity += cartDetailEntity.Quantity;
                currentCartDetail.ShoppingCart.Amount += cartDetailEntity.Quantity * currentCartDetail.Item.Price;
            }
            else
            {
                if (cartDetailEntity.ShoppingCart != null)
                    cartDetailEntity.ShoppingCart.Add();
                await AddAsync(cartDetailEntity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCartDetailEntity> GetByCartIdAndItemId(long cartId, long itemId, CancellationToken ct = default)
        {
            return await GetAsync(
                x =>
                x.ShoppingCartId == cartId &&
                x.ItemId == itemId &&
                x.Status == RecordStatuses.ACTIVE,
                true,
                ct,
                x => x.Item);
        }
    }
}
