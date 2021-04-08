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

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> query)
        {
            return await entities.Where(query).ToListAsync();
        }

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity missing");
            }
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity missing");
            }
            entities.Update(entity);
            //context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity missing");
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
