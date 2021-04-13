using LibraryManager.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        /// <summary>
        /// Gets all entities from the database
        /// </summary>
        /// <returns>An IEnumerable of the specified type</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        /// <summary>
        /// Gets all entities from the database, including foreign objects
        /// </summary>
        /// <param name="includes">A lambda expression specifying the objects to be included</param>
        /// <returns>An IEnumerable of the specified type</returns>
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entities;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets an item of the specified type from the database
        /// </summary>
        /// <param name="id">The Id value to compare against the database</param>
        /// <returns>An object from the database</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        /// <summary>
        /// Gets some items of the specified type from the database, based on a provided lambda expression
        /// </summary>
        /// <param name="predicate">Lambda expression containing the criteria to search for</param>
        /// <returns>An IEnumerable of the specified type</returns>
        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Gets some items of the specified type from the database, based on a provided lambda expression
        /// </summary>
        /// <param name="predicate">Lambda expression containing the criteria to search for</param>
        /// <param name="includes">Lambda expression specifying the objects to be included</param>
        /// <returns>An IEnumerable of the specified type</returns>
        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = entities.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Checks if an item exists in the database
        /// </summary>
        /// <param name="predicate">Lambda expression containing the critera to search for</param>
        /// <returns>True if found, false if not</returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.AnyAsync(predicate);
        }

        /// <summary>
        /// Adds an item to the database
        /// </summary>
        /// <param name="entity">The entity to add</param>
        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an item in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        public async Task UpdateAsync(T entity)
        {
            entities.Update(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an item in the database
        /// </summary>
        /// <param name="entity">The item to be deleted</param>
        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
