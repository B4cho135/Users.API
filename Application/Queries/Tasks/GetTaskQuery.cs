using Application.Models;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Tasks
{
    public class GetTaskQuery : IRequest<OneOf<TaskModel, string>>
    {
        public required string Title { get; set; }
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, OneOf<TaskModel, string>>
    {
        private readonly ITaskService _taskService;

        public GetTaskQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<OneOf<TaskModel, string>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetByTitleAsync(request.Title);
        }
    }
}
