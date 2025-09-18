using Application.Models;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITaskService
    {
        Task<OneOf<TaskModel, string>> CreateAsync(string title);
        Task<OneOf<TaskModel, string>> GetByTitleAsync(string title);
        void ReAssingAndReAvaluate();
    }
}
