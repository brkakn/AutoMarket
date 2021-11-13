using AutoMarket.Api.Features.Authenticate.Commands;
using AutoMarket.Api.Features.Authenticate.Models;
using MediatR;
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

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Post([FromForm] LoginModel model)
        {
            var tokenModel = await _mediator.Send(new AuthenticateCommand(model.username, model.password));
            return Ok(tokenModel);
        }
    }
}
