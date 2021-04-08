using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Repositories
{
    public interface ILibraryDbRepository<T> where T : class
    {
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task<IEnumerable<T>> Select(Expression<Func<T, bool>> query);
        Task Update(T entity);
    }
}