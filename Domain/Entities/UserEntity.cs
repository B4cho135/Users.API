using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserEntity
    {
        public required string Name { get; set; }
        public List<TaskEntity> AssignedTasks { get; set; } = new List<TaskEntity>();
    }
}
