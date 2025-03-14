using Microsoft.EntityFrameworkCore;
using MoviesApi.Core.Consts;
using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class , IEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderDirection = OrderBy.Ascending, string[] includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (includeProperties != null)
                foreach (var include in includeProperties)
                    query = query.Include(include);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();

            return entities;
        }


       public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _context.Set<T>().Where(predicate).ToListAsync();

            return entities;

        }


        public async Task<T> GetByIdWithProperties(int id, string[] includeProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includeProperties.Where(p => !string.IsNullOrWhiteSpace(p)))
            {
                query = query.Include(include);
            }

            
            var entity =  await query.FirstOrDefaultAsync(entity => entity.Id == id);

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
             _context.Update(entity); 
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
