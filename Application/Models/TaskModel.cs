using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TaskModel
    {
        public required string Title { get; set; }
        public required TaskState State { get; set; }
        public string? User { get; set; }
    }
}
