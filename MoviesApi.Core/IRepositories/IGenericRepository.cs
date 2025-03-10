using MoviesApi.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderDirection = OrderBy.Ascending);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);

        T Update(T entity);
        void Delete(T entity);

    }
}
