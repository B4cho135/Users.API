using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserModel
    {
        public required string Name { get; set; }
        public List<TaskModel> AssignedTasks { get; set; } = new List<TaskModel>();
    }
}
