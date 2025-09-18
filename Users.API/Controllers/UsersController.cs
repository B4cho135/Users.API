using Application.Commands.Users;
using Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _mediator.Send(new GetUserQuery { Name = name });

            return result.Match<IActionResult>(success => Ok(success), error => BadRequest(error));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(success => Ok(success), error => BadRequest(error));
        }
    }
}
