using Application.Models;
using Application.Persistance;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using OneOf;

namespace Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly DbContextImitation _dbContext;
        private readonly IMapper _mapper;

        public UsersService(DbContextImitation dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OneOf<UserModel, string>> CreateAsync(UserModel user)
        {
            return await Task.Run(() =>
            {
                var isExistingUser = _dbContext.Users.Any(u => u.Name == user.Name);
                if (isExistingUser) return OneOf<UserModel, string>.FromT1($"User with name {user.Name} already exists");

                var userEntity = new UserEntity
                {
                    Name = user.Name
                };

                _dbContext.Users.Add(userEntity);

                return OneOf<UserModel, string>.FromT0(_mapper.Map<UserModel>(userEntity));
            });
        }

        public UserEntity? FindTaskableUser(TaskEntity taskEntity)
        {
            var taskableUser = _dbContext.Users
                .Where(u => !u.AssignedTasks.Any(at => at == taskEntity))
                .OrderBy(x => x.AssignedTasks.Count)
                .FirstOrDefault();

            return taskableUser;
        }

        public async Task<OneOf<UserModel, string>> GetByNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Name == name);

                if (user == null) return OneOf<UserModel, string>.FromT1($"User with name \"{name}\" could not be found");

                return OneOf<UserModel, string>.FromT0(_mapper.Map<UserModel>(user));
            });
        }
    }
}
