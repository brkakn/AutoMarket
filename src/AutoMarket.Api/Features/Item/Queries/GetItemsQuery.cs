using AutoMapper;
using AutoMarket.Api.Features.Item.Models;
using AutoMarket.Api.Repostories.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.Item.Queries
{
    public class GetItemsQuery : IRequest<List<ItemModel>>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemModel>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemModel>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetAllAsync(ct: cancellationToken);

            return _mapper.Map<List<ItemModel>>(items);
        }
    }
}
