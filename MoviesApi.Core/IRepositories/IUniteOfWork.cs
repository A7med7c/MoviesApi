using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.IRepositories
{
    public interface IUniteOfWork : IDisposable
    {
        public IGenericRepository<Genre> Genres { get;}
        int Save();
    }
}
