using AutoMarket.Api.Features.Authenticate.Commands;
using AutoMarket.Api.Features.Authenticate.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoMarket.Api.Features.Authenticate
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HttpContext _httpContext;

        public AuthenticateController(
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Post([FromForm] LoginModel model)
        {
            var tokenModel = await _mediator.Send(new AuthenticateCommand(model.username, model.password), _httpContext.RequestAborted);
            return Ok(tokenModel);
        }
    }
}
