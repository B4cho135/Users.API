using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssignedTasksHistoryEntity
    {
        public DateTime AssignedAt { get; set; } = DateTime.Now;
        public required UserEntity User { get; set; }
        public required TaskEntity Task { get; set; }
    }
}
