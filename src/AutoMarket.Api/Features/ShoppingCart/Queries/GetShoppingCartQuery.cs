using AutoMapper;
using AutoMarket.Api.Enums;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Models.Exceptions;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart.Queries
{
    public class GetShoppingCartQuery : IRequest<ShoppingCartModel>
    {
        public GetShoppingCartQuery(long userId, long cartId)
        {
            UserId = userId;
            CartId = cartId;
        }

        public long UserId { get; set; }
        public long CartId { get; set; }
    }

    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartModel>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartModel> Handle(GetShoppingCartQuery request, CancellationToken ct)
        {
            var shoppingCart = await _shoppingCartRepository.Query()
                .Include(e => e.ShoppingCartDetails)
                .ThenInclude(e => e.Item)
                .Where(e => e.Id == request.CartId && e.UserId == request.UserId && e.Status == RecordStatuses.ACTIVE)
                .FirstOrDefaultAsync(ct);

            if (shoppingCart == null)
                throw new NotFoundException();

           return _mapper.Map<ShoppingCartModel>(shoppingCart);
        }
    }
}
