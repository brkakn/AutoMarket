using AutoMapper;
using AutoMarket.Api.Entities;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Infrastructures.Mapper;
using AutoMarket.Api.Models.Exceptions;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart.Commands
{
    public class AddItemCommand : IRequest, IMapping
    {
        public ShoppingCartEntity ShoppingCart { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<AddItemCommand, ShoppingCartDetailEntity>()
                .ForMember(dest => dest.ShoppingCart, opt => opt.MapFrom((src, dest) =>
                {
                    return src.ShoppingCart ?? new ShoppingCartEntity() { UserId = src.UserId };
                }));

            profileExpression.CreateMap<AddItemRequestModel, AddItemCommand>();
        }
    }

    public class AddItemCommandHandler : AsyncRequestHandler<AddItemCommand>
    {
        private readonly IShoppingCartDetailRepository _shoppingCartDetailRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddItemCommandHandler(
            IShoppingCartDetailRepository cartDetailRepository,
            IShoppingCartRepository cartRepository,
            IStockRepository stockRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _shoppingCartDetailRepository = cartDetailRepository;
            _shoppingCartRepository = cartRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        protected override async Task Handle(AddItemCommand request, CancellationToken ct)
        {
            var stock = await _stockRepository.GetByItemId(request.ItemId, true, ct);

            if (stock.InsufficientFreeQuantity(request.Quantity))
                throw new UnprocessableException($"Insufficient stock. Current salable stock = {stock.FreeQuantity}");

            request.ShoppingCart = await _shoppingCartRepository.GetByUserIdAsync(request.UserId, true, ct);
            await _shoppingCartDetailRepository.CreateAsync(_mapper.Map<ShoppingCartDetailEntity>(request), ct);

            stock.ReduceFreeQuantity(request.Quantity);
            await _shoppingCartDetailRepository.SaveChangeAsync(ct);

            await _mediator.Send(new UpdateShoppingCartCacheCommand(request.UserId), ct);
        }
    }
}
