using Application.Models;
using Domain.Entities;
using OneOf;

namespace Application.Services
{
    public interface IUsersService
    {
        Task<OneOf<UserModel, string>> GetByNameAsync(string name);
        Task<OneOf<UserModel, string>> CreateAsync(UserModel user);
        UserEntity? FindTaskableUser(TaskEntity taskEntity);
    }
}
