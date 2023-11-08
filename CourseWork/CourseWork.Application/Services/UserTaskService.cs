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
    public class UserTaskService : IUserTaskService
    {
        IUnitOfWork _unitOfWork;
        public UserTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<UserTask>> GetAllAsync()
        {
            return await _unitOfWork.UserTaskRepository.ListAllAsync();
        }
        public Task<UserTask> GetByIdAsync(int id)
        {
            return _unitOfWork.UserTaskRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(UserTask item)
        {
            await _unitOfWork.UserTaskRepository.AddAsync(item);
            await _unitOfWork.SaveAllAsync();
        }
        public async Task UpdateAsync(UserTask item)
        {
            await _unitOfWork.UserTaskRepository.UpdateAsync(item);
            await _unitOfWork.SaveAllAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var userTask = await GetByIdAsync(id);

            if (userTask != null)
            {
                await _unitOfWork.UserTaskRepository.DeleteAsync(userTask);
            }
            await _unitOfWork.SaveAllAsync();
        }
        public async Task DeleteAsync(UserTask item)
        {
            await _unitOfWork.UserTaskRepository.DeleteAsync(item);
            await _unitOfWork.SaveAllAsync();
        }
    }
}
