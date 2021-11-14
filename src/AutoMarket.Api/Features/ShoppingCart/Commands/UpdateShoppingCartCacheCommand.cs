using AutoMapper;
using AutoMarket.Api.Constants;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart.Commands
{
    public class UpdateShoppingCartCacheCommand : IRequest
    {
        public UpdateShoppingCartCacheCommand(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; set; }
    }

    public class UpdateShoppingCartCacheCommandHandler : AsyncRequestHandler<UpdateShoppingCartCacheCommand>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateShoppingCartCacheCommandHandler(
            IShoppingCartRepository shoppingCartRepository,
            ICacheService cacheService,
            IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateShoppingCartCacheCommand request, CancellationToken ct)
        {
            var shoppingCart = await _shoppingCartRepository.GetWithDetailByUserIdAsync(request.UserId, false, ct);

            var shoppingCartModel = _mapper.Map<ShoppingCartModel>(shoppingCart);

            await _cacheService.Add($"{CacheConstants.CartInfo}{request.UserId}", shoppingCartModel);
        }
    }
}
