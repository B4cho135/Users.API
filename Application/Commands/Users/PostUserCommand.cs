using Application.Models;
using Application.Services;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class PostUserCommand : IRequest<OneOf<UserModel, string>>
    {
        public required string Name { get; set; }
    }

    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, OneOf<UserModel, string>>
    {
        private readonly IUsersService _usersService;

        public PostUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<OneOf<UserModel, string>> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserModel
            {
                Name = request.Name
            };
            return await _usersService.CreateAsync(user);
        }
    }
}
