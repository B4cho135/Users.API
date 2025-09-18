using Application.Models;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Tasks
{
    public class PostTaskCommand : IRequest<OneOf<TaskModel, string>>
    {
        public required string Title { get; set; }
    }

    public class PostTaskCommandHandler : IRequestHandler<PostTaskCommand, OneOf<TaskModel, string>>
    {
        private readonly ITaskService _taskService;
        public PostTaskCommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public async Task<OneOf<TaskModel, string>> Handle(PostTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.CreateAsync(request.Title);
        }
    }
}
