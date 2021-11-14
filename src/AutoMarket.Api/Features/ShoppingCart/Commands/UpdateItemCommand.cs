using AutoMapper;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Models.Exceptions;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart.Commands
{
    public class UpdateItemCommand : IRequest, IMapping
    {
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression.CreateMap<UpdateItemRequestModel, UpdateItemCommand>();
        }
    }

    public class UpdateItemCommandHandler : AsyncRequestHandler<UpdateItemCommand>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMediator _mediator;

        public UpdateItemCommandHandler(
            IShoppingCartRepository shoppingCartRepository,
            IStockRepository stockRepository,
            IMediator mediator)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _stockRepository = stockRepository;
            _mediator = mediator;
        }

        protected override async Task Handle(UpdateItemCommand request, CancellationToken ct)
        {
            var shoppingCart = await _shoppingCartRepository.GetWithDetailByUserIdAsync(request.UserId, ct: ct);
            if (shoppingCart == null)
                throw new UnprocessableException("cart not found");

            var cartDetail = shoppingCart.ShoppingCartDetails.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (cartDetail == null)
                throw new UnprocessableException("product not found in cart");

            var stock = await _stockRepository.GetByItemId(request.ItemId, ct: ct);
            var differenceQuantity = request.Quantity - cartDetail.Quantity;
            if (request.Quantity == 0)
            {
                cartDetail.Delete();
                shoppingCart.AddAmount(differenceQuantity * cartDetail.Item.Price);
            }
            else
            {
                if (differenceQuantity > 0 && stock.InsufficientFreeQuantity(differenceQuantity))
                    throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");

                cartDetail.ChangeQuantity(request.Quantity);
            }

            stock.ReduceFreeQuantity(request.Quantity - cartDetail.Quantity);
            await _shoppingCartRepository.SaveChangeAsync(ct);

            await _mediator.Send(new UpdateShoppingCartCacheCommand(request.UserId), ct);
        }
    }
}
