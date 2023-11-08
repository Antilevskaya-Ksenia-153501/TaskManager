using CourseWork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Domain.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        //Search entity by Id
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

        //Get the list of entities
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

        //Get the filtred list
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

        //Add new entity
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        //Change entity
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        //Delete entity
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        //Search for the first entity that satisfies the selection condition.
        //If the entity is not found, the default value will be returned
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken
        cancellationToken = default);
    }
}
