using CourseWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<UserTask> UserTaskRepository { get; }
        IRepository<User> UserRepository { get; }
        public Task CreateDatabaseAsync();
        public Task RemoveDatabaseAsync();
        public Task SaveAllAsync();
    }
}
