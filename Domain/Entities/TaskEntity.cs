using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskEntity
    {
        public required string Title { get; set; }
        public TaskState State { get; set; } = TaskState.Waiting;
        public UserEntity? User { get; set; }
    }
}
