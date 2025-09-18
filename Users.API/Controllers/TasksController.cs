using Application.Commands.Tasks;
using Application.Queries.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var result = await _mediator.Send(new GetTaskQuery { Title = title });

            return result.Match<IActionResult>(success => Ok(success), error => BadRequest(error));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(success => Ok(success), error => BadRequest(error));
        }
    }
}
