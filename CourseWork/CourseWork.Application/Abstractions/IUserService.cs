using CourseWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Application.Abstractions
{
    public interface IUserService : IBaseService<User>
    {
        public Task<User> GetByEmailAsync(string email);
        public Task<IReadOnlyList<UserTask>> GetAllTasksByUserAsync(int id);
        public Task AddTaskByUser(int userId, UserTask task);
    }
}
