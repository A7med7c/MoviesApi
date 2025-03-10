using MoviesApi.Core.IRepositories;
using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.DataAccess.Repositories
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Genre> Genres { get; private set; }
        public IGenericRepository<Movie> Movies { get; private set; }

        public UniteOfWork(ApplicationDbContext context)
        {
            _context = context;
            Genres = new GenericRepository<Genre>(_context);
            Movies = new GenericRepository<Movie>(_context); 
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
