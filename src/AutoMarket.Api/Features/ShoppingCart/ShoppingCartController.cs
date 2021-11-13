using AutoMapper;
using AutoMarket.Api.Features.ShoppingCart.Commands;
using AutoMarket.Api.Features.ShoppingCart.Models;
using AutoMarket.Api.Features.ShoppingCart.Queries;
using AutoMarket.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.ShoppingCart
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/shopping-carts")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserModel _userModel;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public ShoppingCartController(
            IMediator mediator,
            UserModel userModel,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _userModel = userModel;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpPost("add-item")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequestModel model)
        {
            var command = _mapper.Map<AddItemCommand>(model);
            command.UserId = _userModel.Id;

            return Ok(await _mediator.Send(command, _httpContext.RequestAborted));
        }

        [HttpPut("update-item")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 422)]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemRequestModel model)
        {
            var command = _mapper.Map<UpdateItemCommand>(model);
            command.UserId = _userModel.Id;

            return Ok(await _mediator.Send(command, _httpContext.RequestAborted));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _mediator.Send(new GetShoppingCartQuery(_userModel.Id, id)));
        }
    }
}
