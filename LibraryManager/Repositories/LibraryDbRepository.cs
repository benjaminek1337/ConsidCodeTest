using LibraryManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Repositories
{
    public class LibraryDbRepository<T> : ILibraryDbRepository<T> where T : class
    {
        private readonly LibraryDbContext context;
        private DbSet<T> entities;

        public LibraryDbRepository(LibraryDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> query)
        {
            return await entities.Where(query).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        {
            return await entities.AnyAsync(query);
        }

        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            //context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
