using AutoMapper;
using AutoMarket.Api.Constants;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Infrastructures.Cache;
using AutoMarket.Api.Models.Exceptions;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart.Queries
{
    public class GetShoppingCartQuery : IRequest<ShoppingCartModel>
    {
        public GetShoppingCartQuery(long userId, bool fromCache = true)
        {
            UserId = userId;
            FromCache = fromCache;
        }

        public long UserId { get; set; }
        public bool FromCache { get; set; }
    }

    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartModel>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetShoppingCartQueryHandler(
            IShoppingCartRepository shoppingCartRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ShoppingCartModel> Handle(GetShoppingCartQuery request, CancellationToken ct)
        {
            if(request.FromCache)
            {
                var shoppingCartModel = await _cacheService.Get<ShoppingCartModel>($"{CacheConstants.CART_INFO}{request.UserId}");
                if(shoppingCartModel == null)
                {
                    var shoppingCart = await _shoppingCartRepository.GetWithDetailByUserIdAsync(request.UserId, false, ct);
                    await _cacheService.Add($"{CacheConstants.CART_INFO}{request.UserId}", shoppingCart);
                    return _mapper.Map<ShoppingCartModel>(shoppingCart);
                }
                return shoppingCartModel;
            }
            else
            {
                var shoppingCart = await _shoppingCartRepository.GetWithDetailByUserIdAsync(request.UserId, false, ct);

                if (shoppingCart == null)
                    throw new NotFoundException();

                return _mapper.Map<ShoppingCartModel>(shoppingCart);
            }            
        }
    }
}
