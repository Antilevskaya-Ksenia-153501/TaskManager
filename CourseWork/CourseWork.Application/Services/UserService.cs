using CourseWork.Application.Abstractions;
using CourseWork.Domain.Abstractions;
using CourseWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Application.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.ListAllAsync();
        }
        public Task<User> GetByIdAsync(int id)
        {
            return _unitOfWork.UserRepository.GetByIdAsync(id);
        }
        public Task<User> GetByEmailAsync(string email)
        {
            return _unitOfWork.UserRepository.FirstOrDefaultAsync(el => el.Email == email);
        }
        public async Task AddAsync(User item)
        {
            await _unitOfWork.UserRepository.AddAsync(item);
            await _unitOfWork.SaveAllAsync();
        }
        public async Task UpdateAsync(User item)
        {
            await _unitOfWork.UserRepository.UpdateAsync(item);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);

            if (user != null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }
            await _unitOfWork.SaveAllAsync();
        }
        public async Task DeleteAsync(User item)
        {
            await _unitOfWork.UserRepository.DeleteAsync(item);
            await _unitOfWork.SaveAllAsync();

        }
        public Task<IReadOnlyList<UserTask>> GetAllTasksByUserAsync(int id)
        {
            return _unitOfWork.UserTaskRepository.ListAsync(el => el.UserId == id);
        }
        public async Task AddTaskByUser(int userId, UserTask task)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Tasks == null)
            {
                user.Tasks = new List<UserTask>();
            }
            user.Tasks.Add(task);
        }
    }
}
