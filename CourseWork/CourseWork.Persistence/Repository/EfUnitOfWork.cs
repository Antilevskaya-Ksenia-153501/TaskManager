using CourseWork.Domain.Abstractions;
using CourseWork.Domain.Entities;
using CourseWork.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Persistence.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<UserTask>> _userTaskRepository;
        private readonly Lazy<IRepository<User>> _userRepository;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _userTaskRepository = new Lazy<IRepository<UserTask>>(() =>
                                  new EfRepository<UserTask>(context));
            _userRepository = new Lazy<IRepository<User>>(() =>
                              new EfRepository<User>(context));
        }

        IRepository<UserTask> IUnitOfWork.UserTaskRepository =>
        _userTaskRepository.Value;
        IRepository<User> IUnitOfWork.UserRepository =>
        _userRepository.Value;
        public async Task CreateDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
        public async Task RemoveDatabaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
