using AutoMarket.Api.Features.Item.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.Item
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetItemsQuery()));
        }
    }
}
