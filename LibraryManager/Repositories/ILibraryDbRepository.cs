using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Repositories
{
    public interface ILibraryDbRepository<T> where T : class
    {
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> query);
        Task UpdateAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> query);
    }
}