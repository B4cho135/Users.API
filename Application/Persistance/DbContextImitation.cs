using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance
{
    /// <summary>
    /// This class simulates a database context by providing in-memory storage for entities.
    /// Usually, in this layer, I would have just abstraction and actual db functionality implemented in infrastructure layer.
    /// </summary>
    public class DbContextImitation
    {
        public List<UserEntity> Users { get; set; } = new List<UserEntity>();
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
        public List<AssignedTasksHistoryEntity> AssignedTasksHistory {get; set;} = new List<AssignedTasksHistoryEntity>();
    }
}
