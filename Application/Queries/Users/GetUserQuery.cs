using Application.Models;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUserQuery : IRequest<OneOf<UserModel, string>>
    {
        public required string Name { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OneOf<UserModel, string>>
    {
        private readonly IUsersService _usersService;

        public GetUserQueryHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<OneOf<UserModel, string>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _usersService.GetByNameAsync(request.Name);
        }
    }
}
