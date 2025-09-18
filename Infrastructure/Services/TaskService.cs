using Application.Models;
using Application.Persistance;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly DbContextImitation _dbContext;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public TaskService(DbContextImitation dbContext, IMapper mapper, IUsersService usersService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<OneOf<TaskModel, string>> CreateAsync(string title)
        {
            var existingTask = await Task.Run(() => _dbContext.Tasks.Any(t => t.Title == title));

            if (existingTask) return $"Task with title {title} already exists";

            var taskEntity = new TaskEntity
            {
                Title = title
            };

            var user = await Task.Run(() => _usersService.FindTaskableUser(taskEntity));

            taskEntity.State = user == null ? TaskState.Waiting : TaskState.InProgress;
            taskEntity.User = user;

            if(user != null)
            {
                user.AssignedTasks.Add(taskEntity);

                _dbContext.AssignedTasksHistory.Add(new AssignedTasksHistoryEntity()
                {
                    User = user,
                    Task = taskEntity
                });
            }

            _dbContext.Tasks.Add(taskEntity);

            return await Task.Run(() => _mapper.Map<TaskModel>(taskEntity));
        }

        public async Task<OneOf<TaskModel, string>> GetByTitleAsync(string title)
        {
            return await Task.Run(() =>
            {
                var task = _dbContext.Tasks.FirstOrDefault(t => t.Title == title);

                if (task == null) return OneOf<TaskModel, string>.FromT1($"Task with title \"{title}\" could not be found");

                return OneOf<TaskModel, string>.FromT0(_mapper.Map<TaskModel>(task));
            });
        }

        public void ReAssingAndReAvaluate()
        {
            var totalUsers = _dbContext.Users.Count;

            var currentTasks = _dbContext.Tasks.Where(t => t.State != TaskState.Completed).ToList();

            foreach (var task in currentTasks)
            {
                var userCountForTask = _dbContext.AssignedTasksHistory.Count(t => t.User == task.User);

                if(totalUsers <= userCountForTask)
                {
                    task.State = TaskState.Completed;
                    task.User!.AssignedTasks.Remove(task);
                    task.User = null;

                    continue;
                }
                else
                {
                    var user = _usersService.FindTaskableUser(task);

                    if(user != null)
                    {
                        task.User?.AssignedTasks.Remove(task);

                        user.AssignedTasks.Add(task);

                        _dbContext.AssignedTasksHistory.Add(new AssignedTasksHistoryEntity()
                        { 
                            User = user,
                            Task = task
                        });
                    }

                    task.State = user == null ? TaskState.Waiting : TaskState.InProgress;
                    task.User = user;
                }
            }
        }
    }
}
