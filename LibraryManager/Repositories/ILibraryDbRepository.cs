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
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> query);
        Task UpdateAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> query);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}